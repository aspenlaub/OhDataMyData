using System.Collections.Generic;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Entities;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Interfaces {
    public interface IOhMyRepository {
        IList<OhMyEntity> GetEntities();
        // ReSharper disable once UnusedMemberInSuper.Global
        void Add(OhMyEntity entity);
    }
}