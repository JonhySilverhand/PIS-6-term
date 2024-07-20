using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASPCMVC08.Exceptions;

public class ExceptionHandler : Exception
{
    public IEnumerable<string> ErrorsHans { get; init; }


    public ExceptionHandler(ModelStateDictionary modelState) : base()
    {
        var values = modelState.Values;
        var list = new List<string>();
        var error = string.Empty;

        foreach (var v in values)
            list.AddRange(v.Errors.Select(e => e.ErrorMessage));

        ErrorsHans = list;
    }
}
