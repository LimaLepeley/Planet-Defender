using UnityEngine;

//* Clase de pruebas hecha por AI
public class PlayerTest : MonoBehaviour
{
    void Start()
    {
        pruebas();
    }

    // Script que sirve para probar que todos los scripts necesarios están activos
    private void pruebas()
    {
        System.Type[] scriptsRequeridos = {
            typeof(PlayerAnimation),
            typeof(PlayerAttacks),
            typeof(PlayerCollision),
            typeof(PlayerEnergy),
            typeof(PlayerMovement),
            typeof(PlayerSound),
            typeof(PlayerUpgrades)
        };

        foreach (System.Type script in scriptsRequeridos)
        {
            if (!TryGetComponent(script, out _))
            {
                Debug.LogError($"Falta el script {script.Name} en el jugador.");
            }
        }
    }
}