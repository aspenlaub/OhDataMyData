using System.Collections.Generic;
// ReSharper disable CollectionNeverUpdated.Global

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Test {
    public class ODataResponse<T> {
        public List<T> Value { get; set; }
    }
}
