using ProjectDto.Dtos.EscanerDtos;
using ProjectCommon.Constants;
using System;

namespace ProjectServices.Parser
{
    public class CedulaParser : IEscanParser
    {
        public PacienteEscaneadoDto Parse(string lectura)
        {
            string[] datos = lectura.Trim().Split('\t');

            if (datos.Length < 7)
            {
                throw new Exception("La lectura del documento es inválida.");
            }

            return new PacienteEscaneadoDto
            {
                IdTipoDocumento = 2,
                DescripcionTipoDocumento = "Cédula de Ciudadanía",

                NroDocumento = LimpiarNumeroDocumento(datos[0]),

                PrimerApellido = datos[1],
                SegundoApellido = datos[2],

                PrimerNombre = datos[3],
                SegundoNombre = datos[4],

                IdGenero = ObtenerGenero(datos[5]),

                FechaNacimiento = ObtenerFechaNacimiento(datos[6])
            };
        }

        private int ObtenerGenero(string dato)
        {
            if (dato == "M")
            {
                return (int)Generos.Masculino;
            }

            return (int)Generos.Femenino;
        }

        private DateTime ObtenerFechaNacimiento(string dato)
        {
            // Cedula nueva
            // 080414
            if (dato.Length == 6)
            {
                int año = 1900 + int.Parse(dato.Substring(0, 2));

                if (año < 1950)
                {
                    año += 100;
                }

                int mes = int.Parse(dato.Substring(2, 2));
                int dia = int.Parse(dato.Substring(4, 2));

                return new DateTime(año, mes, dia);
            }

            // Cedula vieja
            // 19800311
            if (dato.Length == 8)
            {
                int año = int.Parse(dato.Substring(0, 4));
                int mes = int.Parse(dato.Substring(4, 2));
                int dia = int.Parse(dato.Substring(6, 2));

                return new DateTime(año, mes, dia);
            }

            throw new Exception("Formato de fecha inválido.");


        }

        private string LimpiarNumeroDocumento(string numero)
        {
            string limpio = numero.TrimStart('0');

            return string.IsNullOrEmpty(limpio) ? "0" : limpio;
        }
    }
}