namespace Database_VmProject.Services
{
    public class QueryParamHelper
    {
        public static List<string> ValidateParameters(params (string paramName, dynamic paramValue)[] paramList)
        {
            List<string> validParameters = new();
            foreach ((string paramName, dynamic paramValue) param in paramList)
            {
                if (param.paramValue != null)
                {
                    validParameters.Add(param.paramName);
                }
            }
            return validParameters;
        }

    }
}
