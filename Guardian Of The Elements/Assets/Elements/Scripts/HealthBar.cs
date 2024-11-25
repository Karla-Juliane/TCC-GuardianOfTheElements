using Elements.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Elements.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        
        PlayerController player;
        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            slider.value = player.life;
        }
    }
}
