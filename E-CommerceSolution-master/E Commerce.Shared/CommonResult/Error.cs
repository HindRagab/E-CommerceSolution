namespace E_Commerce.Shared.CommonResult
{
    public class Error
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorType Type { get; }
        private Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
        }

        #region Static Factory Methods

        public static Error Failure(string Code = "General.Failure", string Description = "A General Failure Has Occurred")
        {
            return new Error(Code, Description, ErrorType.Failure);
        }

        public static Error Validation(string Code = "General.Validation", string Description = "A Validation Error Has Occurred")
        {
            return new Error(Code, Description, ErrorType.Validation);
        }

        public static Error NotFound(string Code = "General.NotFound", string Description = "The Requested Resource Was Not Found")
        {
            return new Error(Code, Description, ErrorType.NotFound);
        }

        public static Error Unauthorized(string Code = "General.Unauthorized", string Description = "You Are Not Authorized To Access This Resource")
        {
            return new Error(Code, Description, ErrorType.Unauthorized);
        }

        public static Error Forbidden(string Code = "General.Forbidden", string Description = "Access To This Resource Is Forbidden")
        {
            return new Error(Code, Description, ErrorType.Forbidden);
        }
        public static Error InvalidCredentials(string Code = "General.InvalidCredentials", string Description = "The Provided Credentials Are Invalid")
        {
            return new Error(Code, Description, ErrorType.InvalidCredentials);
        } 
        #endregion

    }
}
