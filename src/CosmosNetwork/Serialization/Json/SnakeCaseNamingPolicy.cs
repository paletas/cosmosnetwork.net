using System.Text;
using System.Text.Json;

namespace CosmosNetwork.Serialization.Json
{
    internal class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            StringBuilder builder = new();
            foreach (char @char in name)
            {
                if (@char is >= 'A' and <= 'Z')
                {
                    if (builder.Length > 0)
                    {
                        _ = builder.Append('_');
                    }

                    _ = builder.Append((char)(@char + 0x20));
                }
                else
                {
                    _ = builder.Append(@char);
                }
            }
            return builder.ToString();
        }
    }
}
