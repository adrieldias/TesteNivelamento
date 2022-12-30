namespace Questao5.Domain.Dtos
{
    public class ResponseDto
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public ResponseDto(bool success, object data)
        {
            Success = success;
            Data = data;    
        }
    }
}
