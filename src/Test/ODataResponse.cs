using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Test {
    public class ODataResponse<T> {
        [JsonPropertyName("value")]
        public List<T> Value { get; set; } = new();
    }
}
