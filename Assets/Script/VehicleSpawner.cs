using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] vehicleReference; // Array untuk menyimpan referensi prefab kendaraan

    [SerializeField]
    private int direction; // Arah gerakan kendaraan (positif atau negatif)

    [SerializeField]
    private int[] speed = {7,5,6}; // Array untuk menyimpan kecepatan masing-masing jenis kendaraan
    private GameObject spawnedVehicle;  // Referensi untuk kendaraan yang baru saja di-spawn

    private int randomIndex; // Indeks acak untuk memilih kendaraan dan kecepatannya

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnVehicles()); // Memulai coroutine untuk memunculkan kendaraan secara berkala
    }

    
    IEnumerator SpawnVehicles(){    // Coroutine untuk memunculkan kendaraan secara terus-menerus
        while (!GameManager.instance.CharStatus){
            // Debug.Log("Player alive? :" + GameManager.instance.CharStatus);
            yield return new WaitForSeconds(Random.Range(1,5));

            randomIndex = Random.Range(0, vehicleReference.Length);

            spawnedVehicle = Instantiate(vehicleReference[randomIndex]);
            spawnedVehicle.transform.position = transform.position;
            // Vector3 scale = spawnedVehicle.transform.localScale;
            // scale.x *= direction;
            // spawnedVehicle.transform.localScale = scale;
            
            // Tetapkan kecepatan gerakan kendaraan berdasarkan arah dan kecepatan yang sudah ditentukan
            spawnedVehicle.GetComponent<Vehicle>().speed = - direction * speed[randomIndex];
        } // while loop
    }
    

} //class
