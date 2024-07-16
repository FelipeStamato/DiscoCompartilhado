using System;
using System.Linq;
using System.Management;

namespace DiscoCompartilhadoBIOS.Handler
{
    public class DiscoHandler
    {
        public string consulta;

        public DiscoHandler(string consulta)
        {
            this.consulta = consulta;
        }

        public ArmazenamentoLogico Executa()
        {
            try
            {
                if (consulta == null)
                {
                    consulta = "SELECT * FROM Win32_LogicalDisk";
                }

                ArmazenamentoLogico disco = new ManagementObjectSearcher(consulta)
                    .Get().
                    Cast<ManagementBaseObject>()
                    .Select(x => new ArmazenamentoLogico
                    {
                        Nome = x["Name"] as string,
                        Descricao = x["Description"] as string,
                        CapacidadeBytes = (long)(x["Size"] != null ? Convert.ToInt64(x["Size"]) : (long?)null),
                        EspacoLivre = (long)(x["FreeSpace"] != null ? Convert.ToInt64(x["FreeSpace"]) : (long?)null),
                        Serial = x["VolumeSerialNumber"] as string,
                        SistemaArquivos = x["FileSystem"] as string,
                        TipoMidia = (int)(x["MediaType"] != null ? (int?)Convert.ToInt32(x["MediaType"]) : null),
                        TipoDriver = (int)(x["DriveType"] != null ? (int?)Convert.ToInt32(x["DriveType"]) : null),
                        EnderecoRede = PegaEnderecoRede()
                            
                    }).FirstOrDefault();
                return disco;
            }
            catch (Exception ex)
            {
                // Log de exceção
            }

            return null;
        }
        private string PegaEnderecoRede()
        {
            try
            {
                string consultaNetWorkAdapter = "SELECT * FROM Win32_NetworkAdapterConfiguration";
                ManagementObjectSearcher netWorkAdapterSearcher = new ManagementObjectSearcher(consultaNetWorkAdapter);
                ManagementObjectCollection netWorkAdapterCollection = netWorkAdapterSearcher.Get();

                foreach (ManagementObject netWorkAdapter in netWorkAdapterCollection)
                {
                    if (netWorkAdapter["IPAddress"] != null)
                    {
                        string[] enderecosIP = (string[])netWorkAdapter["IPAddress"];
                        if (enderecosIP.Length > 0)
                        {
                            return enderecosIP[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log de exceção
            }

            return string.Empty;
        }
    }
}
