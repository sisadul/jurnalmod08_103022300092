using System.Text.Json;

namespace jurnal08_103022300092
{
    class Transfer 
    {
        public int threshold { get; set; }
        public int low_fee { get; set; }
        public int high_fee { get; set; }
    }
    class Confirmation
    { 
        public string en { get; set; }
        public string id { get; set; }
    }
    class BankTransferConfig
    {
        public string filePath = "bank_transfer_config.json";
        public Config config { get; set; }
        public BankTransferConfig()
        {
            try
            {
                ReadConfigFile();
            }
            catch (Exception)
            {
                setDefault();
                WriteNewConfigFile();
            }
        }
        public void setDefault()
        { 
            config = new Config();
            config.lang = "en";
            config.transfer.threshold = 25000000;
            config.transfer.high_fee = 15000;
            config.transfer.low_fee = 6500;
            config.confirmation.en = "yes";
            config.confirmation.id = "ya";
            config.methods = ["RTO (real-tome)", "SKN", "RTGS", "BI FAST"];
        }
        public class Config
        {
            public string lang { get; set; } 
            public Transfer transfer { get; set; }
            public List<String> methods { get; set; }
            public Confirmation confirmation { get; set; } 
        }
        public Config ReadConfigFile()
        { 
            String configJsonData = File.ReadAllText(filePath);
            config = JsonSerializer.Deserialize<Config>(configJsonData);
            return config;
        }
        public void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            String jsonString = JsonSerializer.Serialize(config, options);
            File.WriteAllText(filePath, jsonString);
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {

            BankTransferConfig config = new BankTransferConfig();
            config.ReadConfigFile();

            if (config.config.lang == "en")
            {
                Console.WriteLine("Please insert the amount of money to transfer");
                int input = Convert.ToInt32(Console.ReadLine());
                if (input <= config.config.transfer.threshold)
                {
                    Console.WriteLine("Low fee:");
                    Console.WriteLine($"Transfer fee = {config.config.transfer.low_fee}, Total amount = {config.config.transfer.low_fee + input}");
                }
                else 
                {
                    Console.WriteLine("high fee:");
                    Console.WriteLine($"Transfer fee = {config.config.transfer.high_fee}, Total amount = {config.config.transfer.high_fee + input}");
                }
                Console.WriteLine("Select transfer method");
                foreach (var item in config.config.methods)
                {
                    Console.WriteLine(item);
                }
                int n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Pleaste type {config.config.confirmation.en}");
                Console.WriteLine("Transfer is complate");
            }
            else if (config.config.lang == "id")
            {
                Console.WriteLine("Masukan jumlah uang yang akan di-transefr");
                int input = Convert.ToInt32(Console.ReadLine());
                if (input <= config.config.transfer.threshold)
                {
                    Console.WriteLine("Low fee:");
                    Console.WriteLine(config.config.transfer.low_fee + input);
                }
                else
                {
                    Console.WriteLine("high fee:");
                    Console.WriteLine(config.config.transfer.high_fee + input);
                }
                Console.WriteLine("Pilih metode transfer");
                foreach (var item in config.config.methods)
                {
                    Console.WriteLine(item);
                }
                int n =  Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Ketik {config.config.confirmation.id}");
                Console.WriteLine("Proses transefer berhasil");
            }

        }
    }
}
