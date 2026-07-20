using Newtonsoft.Json;
using ProjectServices.AI;
using ProjectServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServices.Implementations
{
    public class GeminiService : IIAService
    {
        private readonly string _apiKey;

        private readonly string _urlBase;
        
        private readonly HttpClient _httpClient;

       
        public GeminiService()
        {
            _apiKey = ConfigurationManager.AppSettings["GeminiApiKey"];

            _urlBase = ConfigurationManager.AppSettings["GeminiUrl"];


            //Validar que Existan las configuraciones en el WebConfig

            if (string.IsNullOrWhiteSpace(_apiKey))
                throw new Exception("No se encontró la configuración GeminiApiKey.");

            if (string.IsNullOrWhiteSpace(_urlBase))
                throw new Exception("No se encontró la configuración GeminiUrl.");


            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        public async Task<string> GenerarRespuestaAsync(string prompt)
        {

            try
            {
                //Estructura de peticiones en Gemini
                GeminiRequest request = ConstruirRequest(prompt);


                //Enviar Peticion Http Post
                HttpResponseMessage response = await EnviarRequest(request);


                //Obtiene la respuesta luego de deserializar el JSON que devuelve la IA en un objeto GeminiResponse
                GeminiResponse geminiResponse = await ObtenerRespuesta(response);


                //Devuelve solo el texto que contiene la respuesta
                return ExtraerRespuesta(geminiResponse);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al comunicarse con Gemini: {ex.Message}", ex);
            }
            
        }


        //Metodos Privados de Validaciones, Serializaciones, eserializaciones y Peticiones Post
        private GeminiRequest ConstruirRequest(string prompt)
        {
            return new GeminiRequest
            {
                Contents = new List<Content>
                    {
                        new Content
                        {
                            Parts = new List<Part>
                            {
                                new Part
                                {
                                    Text = prompt
                                }
                            }
                        }
                    }
            };
        }

        private async Task<HttpResponseMessage> EnviarRequest(GeminiRequest request)
        {
            //Serializar el Request
            string jsonRequest = JsonConvert.SerializeObject(request);


            //Para decirle a la IA que tipo de dato le envio y como esta codificado "Lo que realmente recibe la peticion Http"
            StringContent content = new StringContent(
                jsonRequest,
                Encoding.UTF8,
                "application/json");


            //Peticion tipo Post(Ya que estamos enviando información para que sea procesada)
            HttpResponseMessage response = await _httpClient.PostAsync($"{_urlBase}?key={_apiKey}", content);


            //Validacion de que la peticion si haya sido exitosa
            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();

                throw new Exception(
                    $"Gemini respondió {(int)response.StatusCode} - {response.ReasonPhrase}. Detalle: {error}");
            }

            return response;
        }

        private async Task<GeminiResponse> ObtenerRespuesta(HttpResponseMessage response)
        {
            //Extrae el contenido de la respuesta en JSON"string" que devolvió la IA
            string jsonResponse = await response.Content.ReadAsStringAsync();


            //Deserializa el JSON respuesta de la IA en objeto C#
            GeminiResponse geminiResponse = JsonConvert.DeserializeObject<GeminiResponse>(jsonResponse);


            //Validar que el JSON realmente exista
            if (geminiResponse == null)
            {
                throw new Exception("No fue posible interpretar la respuesta de Gemini.");
            }

            return geminiResponse;
        }

        private string ExtraerRespuesta(GeminiResponse response)
        {
            //Navegar en nuestra clase C# para obtener solo el texto respuesta. Primero Valida que no venga vacío
            if (response.Candidates == null || !response.Candidates.Any())
            {
                throw new Exception("Gemini no devolvió candidatos.");
            }

            Candidate candidate = response.Candidates.First();

            //Validar Content
            if (candidate.Content == null)
            {
                throw new Exception("La respuesta de Gemini no contiene contenido.");
            }

            //Validar Parts
            if (candidate.Content.Parts == null || !candidate.Content.Parts.Any())
            {
                throw new Exception("La respuesta de Gemini no contiene partes.");
            }


            //Validar Texto respuesta
            string respuesta = candidate.Content.Parts.First().Text;

            if (string.IsNullOrWhiteSpace(respuesta))
            {
                throw new Exception("Gemini devolvió una respuesta vacía.");
            }

            return respuesta.Trim().ToUpper();
        }
    }
}