namespace API.Errores
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string mensaje=null)
        {
            StatusCode = statusCode;
            Mensaje = mensaje ?? GetMensajeStatusCode(StatusCode);
        }

        public int StatusCode { get; set; }

        public string Mensaje { get; set; }

        private string GetMensajeStatusCode(int statusCode)
        {

            return statusCode switch
            {
                400 => "Se ha realizado un solicitud no validad",
                401 => "No estas autorizado para este recuros",
                404 => "Recurso no encontrado",
                500 => "Error interno del servidor",
                _ => null
            };
        }
    }
}
