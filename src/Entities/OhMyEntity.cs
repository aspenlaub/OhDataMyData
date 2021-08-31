using System;
using System.ComponentModel.DataAnnotations;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Entities {
    public class OhMyEntity {
        [Key]
        public string Id { get; set;  } = Guid.NewGuid().ToString();

        public string Name { get; init; }
    }
}
