using System.Collections.Generic;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Test {
    public class ODataResponse<T> {
        public List<T> Value { get; set; }
    }
}
