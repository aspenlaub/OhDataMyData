using System;
using System.Collections.Generic;
using System.Linq;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Entities;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Interfaces;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Components {
    public class OhMyRepository : IOhMyRepository {
        private readonly IList<OhMyEntity> Entities;

        public OhMyRepository() {
            Entities = new List<OhMyEntity>();
        }

        public IList<OhMyEntity> GetEntities() {
            return Entities;
        }

        public void Add(OhMyEntity entity) {
            if (Entities.Any(e => e.Id == entity.Id)) {
                throw new ArgumentException("The same entity has already been added");
            }

            Entities.Add(entity);
        }
    }
}
