namespace MovieGrpc.Crosscutting.Commom
{
    public static class Message
    {
        public static string TRACE_METHOD = $"Request Received for MovieService::";
        public static string ERRO_METHOD = $"Request Failure Received for MovieService::";


        public static string ERRO_METHOD_EXPECTED_FAILURE = $"Expected failure";
    }
}