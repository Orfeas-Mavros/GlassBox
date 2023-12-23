using Newtonsoft.Json;
using NeuralNetworks;

namespace NetworkImportExport
{
    public static class Import
    {
        // To be tested at a later stage, along with Problem Space and Database Imports
    }


    // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ // ~~~ //


    public static class Export
    {
        public static string JSON(NetworkData toExport)
        {
            return JsonConvert.SerializeObject(toExport);
        }
        public static string JSON(NeuralNet toExport)
        {
            return JSON(toExport.NetData);
        }
    }
}