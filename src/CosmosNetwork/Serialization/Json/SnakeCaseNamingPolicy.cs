using System.Text;
using System.Text.Json;

namespace CosmosNetwork.Serialization.Json
{
    internal class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var @char in name)
            {
                if (@char >= 'A' && @char <= 'Z')
                {
                    if (builder.Length > 0) builder.Append('_');
                    builder.Append((char)(@char + 0x20));
                }
                else builder.Append(@char);
            }
            return builder.ToString();
        }
    }
}
