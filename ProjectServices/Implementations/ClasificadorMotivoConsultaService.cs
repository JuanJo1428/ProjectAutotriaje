using ProjectData.Entities.PretriajeModel;
using ProjectData.Repositories.Implementations;
using ProjectData.Repositories.Interfaces.PretriajeInterfaces;
using ProjectDto.Dtos.PretriajeDtos;
using ProjectServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServices.Implementations
{
    public class ClasificadorMotivoConsultaService : IClasificadorMotivoConsultaService
    {
        private readonly IIAService _iaService;
        private readonly IFlujoPretriajeRepository _flujoRepository;

        public ClasificadorMotivoConsultaService(IIAService iaService, IFlujoPretriajeRepository flujoPretriaje)
        {
            _iaService = iaService;
            _flujoRepository = flujoPretriaje;
        }

        public ClasificadorMotivoConsultaService()
        {
            _iaService = new GeminiService();
            _flujoRepository = new FlujoPretriajeRepository();
        }

        public async Task<string> ClasificarMotivoConsultaAsync(SolicitudPretriajeDto solicitud)
        {
            string prompt = ConstruirPromptClasificacion(solicitud);

            string codigo = await _iaService.GenerarRespuestaAsync(prompt);


            FlujoPretriaje flujo = _flujoRepository.ObtenerPorCodigo(codigo);

            if (flujo == null)
            {
                return null;
            }

            return flujo.Codigo;

        }

        private string ConstruirPromptClasificacion(SolicitudPretriajeDto solicitud)
        {
            List<FlujoPretriaje> flujos = _flujoRepository.ObtenerTodos();

            //Crear un string modificable inicialmente vacío
            StringBuilder flujosPrompt = new StringBuilder();

            foreach (FlujoPretriaje flujo in flujos)
            {
                flujosPrompt.AppendLine($"{flujo.Nombre}:{flujo.Codigo} -> {flujo.Descripcion}");
            }

            return $@"
                ========================================
                ROL
                ========================================

                Eres un médico con amplia experiencia en TRIAGE y PRETRIAJE hospitalario.

                Tu única función es clasificar el motivo de consulta de un paciente dentro del flujo de pretriaje más adecuado.

                No eres un asistente conversacional.

                No debes responder preguntas.

                No debes explicar tus decisiones.

                Únicamente debes clasificar.

                ========================================
                OBJETIVO
                ========================================

                Analiza cuidadosamente el motivo de consulta escrito por el paciente.

                Determina cuál flujo de pretriaje debe iniciarse.

                Debes devolver únicamente el código del flujo seleccionado.

                ========================================
                REGLAS OBLIGATORIAS
                ========================================

                • Responde únicamente UN código.
                • Nunca expliques tu decisión.
                • Nunca escribas frases.
                • Nunca agregues puntuación.
                • Nunca uses comillas.
                • Nunca agregues espacios innecesarios.
                • Nunca combines varios códigos.
                • Nunca inventes códigos.
                • Nunca respondas ""No aplica"", ""No identificado"" o respuestas similares.
                • Tu respuesta debe contener exactamente un único código válido perteneciente a los flujos disponibles.
                • Nunca modifiques el formato del código.
                • Respeta exactamente las letras y números del código seleccionado.

                ========================================
                CRITERIOS DE PRIORIZACIÓN
                ========================================

                Es posible que el paciente describa síntomas compatibles con varios flujos.

                En ese caso:

                1. Identifica todos los síntomas presentes.
                2. Analiza el riesgo clínico de cada uno.
                3. Selecciona únicamente el flujo MÁS IMPORTANTE.
                4. Ignora los síntomas secundarios.

                Siempre prioriza:

                • Riesgo para la vida.
                • Compromiso respiratorio.
                • Compromiso cardiovascular.
                • Alteración neurológica.
                • Hemorragias.
                • Trauma mayor.
                • El síntoma predominante.
                • La condición que requiere atención más urgente.

                NO selecciones:

                • El síntoma más mencionado.
                • El primero que aparezca.
                • El síntoma menos grave.

                Si el motivo de consulta parece compatible con varios flujos, selecciona el flujo específico que represente el mayor riesgo clínico o el síntoma predominante.

                Solo si ningún flujo específico resulta clínicamente razonable podrás utilizar el flujo de respaldo.
                
                Antes de responder:

                1. Analiza completamente el motivo de consulta.
                2. Identifica todos los síntomas descritos.
                3. Evalúa la gravedad relativa de cada uno.
                4. Determina cuál representa el mayor riesgo clínico.
                5. Compara el caso con TODOS los flujos disponibles.
                6. Selecciona el flujo específico que tenga mayor coherencia clínica.

                No tomes decisiones basándote únicamente en palabras clave.

                Analiza siempre el contexto completo del motivo de consulta.
                
                ========================================
                USO DEL FLUJO DE RESPALDO
                ========================================

                Existe un flujo denominado ""Otros síntomas generales y no clasificables"" cuyo único propósito es actuar como mecanismo de seguridad cuando realmente no exista una relación clínica razonable con ninguno de los demás flujos disponibles.

                Este flujo representa la ÚLTIMA alternativa de clasificación.

                Antes de seleccionarlo debes:

                1. Comparar el motivo de consulta con TODOS los flujos disponibles.
                2. Evaluar si alguno presenta una relación clínica razonable, aunque no sea perfecta.
                3. Seleccionar siempre el flujo específico que mejor represente el síntoma predominante y el mayor riesgo clínico.

                Nunca selecciones un flujo genérico si existe un flujo específico que describa el sistema corporal, órgano o condición clínica predominante del paciente.

                Siempre prioriza la especificidad clínica sobre las categorías generales.

                NO utilices el flujo de respaldo únicamente porque:

                • El motivo de consulta esté redactado de forma informal.
                • Existan errores ortográficos.
                • El paciente utilice lenguaje cotidiano.
                • El caso no coincida exactamente con un ejemplo.
                • Existan síntomas compatibles con más de un flujo.
                • Tengas dudas leves entre dos flujos.
                • El motivo de consulta sea corto.
                • El paciente no utilice terminología médica.

                El flujo de respaldo SOLO puede utilizarse cuando, después de analizar cuidadosamente el motivo de consulta completo, determines que NO existe una relación clínica suficientemente razonable con ninguno de los demás flujos específicos.

                Si existe un flujo que explique el caso aunque sea parcialmente, SIEMPRE debes preferir ese flujo específico antes que el flujo de respaldo.

                Únicamente utiliza el flujo ""Otros síntomas generales y no clasificables"" cuando la clasificación en cualquier otro flujo implique forzar una relación clínica inexistente.

                ========================================
                CATÁLOGO OFICIAL DE FLUJOS CLÍNICOS
                ========================================

                Cada flujo contiene un nombre, un código y una descripción clínica.

                No bases tu decisión únicamente en el nombre del flujo.

                Utiliza la descripción completa de cada flujo para comprender el alcance clínico de cada categoría.

                Aplica además todo tu conocimiento médico, clínico y de triaje para determinar cuál flujo representa de forma más precisa el motivo de consulta del paciente.

                La clasificación debe fundamentarse en el análisis clínico integral del caso y posteriormente seleccionar el flujo cuya descripción tenga la mayor coherencia con dicho análisis.

                {flujosPrompt}

                ========================================
                CASOS CLÍNICOS DE REFERENCIA
                ========================================

                Los siguientes casos tienen únicamente un propósito de aprendizaje y referencia.

                NO debes memorizar estos ejemplos.

                NO debes limitar tu razonamiento a ellos.

                Cada motivo de consulta debe analizarse de forma independiente utilizando criterio clínico y aplicando las reglas de priorización descritas anteriormente.

                Los ejemplos NO representan todos los escenarios posibles.

                Es posible que el motivo de consulta contenga síntomas diferentes, múltiples condiciones o una redacción distinta.

                Siempre fundamenta tu decisión en el análisis clínico del caso actual y no en la similitud con los ejemplos.

                Caso 1

                Motivo:
                Tengo mucho dolor abdominal pero ahora siento una presión muy fuerte en el pecho.

                Flujo seleccionado:
                DT001


                Caso 2

                Motivo:
                Me golpeé una pierna y además siento que no puedo respirar.

                Flujo seleccionado:
                DI002


                Caso 3

                Motivo:
                Me caí de una escalera y tengo una herida profunda en la cabeza.

                Flujo seleccionado:
                TR003


                Caso 4

                Motivo:
                Tengo tos desde hace tres días pero desde hace una hora siento mucha dificultad para respirar.

                Flujo seleccionado:
                DI002

                ========================================
                MOTIVO DE CONSULTA
                ========================================

                {solicitud.MotivoConsulta}

                ========================================
                RESPUESTA
                ========================================

                Devuelve únicamente el código del flujo exactamente como aparece en el catálogo oficial de flujos clínicos.
                ";

        }
    }
}