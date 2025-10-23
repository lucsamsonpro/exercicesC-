using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SimpleJSON;
using UnityEngine;

namespace Helpers
{
    public static class ScriptChecker
    {
        public static async Task<string> CheckScriptAsync(string apiKey, string scriptContent)
        {
            string systemPrompt = "Voici un script C# pour Unity. Il s'agit d'un exercice avec l'énoncé et le code complété. Base toi sur le contenu pour me dire si le code est correct ou non. Le premier mot de ta réponse doit être CORRECT ou INCORRECT selon le cas suivi d'un espace. Puis complète ta réponse avec des détails sur l'erreur en cas de réponse INCORRECT. Ne donne pas la solution et fais une réponse courte. Exemple de réponse : INCORRECT Il manque un point-virgule à la fin de la ligne 5.";
            string escapedScriptContent = scriptContent.Replace("\n", "\\n").Replace("\r", "").Replace("\"", "\\\"");
            string jsonBody = "{" +
                "\"messages\": [" +
                    "{\"role\": \"system\", \"content\": \"" + systemPrompt.Replace("\"", "\\\"") + "\"}," +
                    "{\"role\": \"user\", \"content\": \"Vérifie ce code : " + escapedScriptContent + "\"}" +
                "]," +
                "\"model\": \"gemma2-9b-it\"," +
                "\"temperature\": 0.1" +
            "}";
            
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                string url = "https://api.groq.com/openai/v1/chat/completions";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string responseJson = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    Debug.LogError("Erreur d'appel API Groq : " + response.StatusCode + " " + responseJson);
                    return null;
                }
                JSONNode root = JSON.Parse(responseJson);
                if (root["choices"] != null && root["choices"].Count > 0)
                {
                    JSONNode firstChoice = root["choices"][0];
                    if (firstChoice["message"] != null && firstChoice["message"]["content"] != null)
                    {
                        return firstChoice["message"]["content"].Value.Trim();
                    }
                }
                Debug.Log("Réponse API Groq: structure de réponse invalide.");
                return null;
            }
        }
    }
}
