namespace Todo.Application.Common.Model;

public class ResponseModel
{
    public ResponseModel(bool status, string message)
    {
        Status = status;
        Message = message;
    }

    public bool Status { get; set; }
    public string Message { get; set; }
}