using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Game.Holes
{
    public class HolesField : MonoBehaviour
    {
        [SerializeField] private List<HoleView> _holes;

        public List<HoleView> Holes => _holes;

        private void OnValidate() => _holes = GetComponentsInChildren<HoleView>().ToList();
    }
}
