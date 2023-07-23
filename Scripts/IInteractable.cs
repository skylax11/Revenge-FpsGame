using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.Scripts
{
    public interface IInteractable
    {
        public string name { get; set; }
        void interact();
    }
}
