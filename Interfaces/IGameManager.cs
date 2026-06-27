using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberHunt.Interfaces
{
    internal interface IGameManager
    {
        void Start();
        void Update();
        void EnemyShoot();
    }
}
