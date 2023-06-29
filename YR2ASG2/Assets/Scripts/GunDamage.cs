/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The GunDamage class is used for damages done by gun
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDamage : MonoBehaviour
{
    public float Damage;
    public float BulletRange;
    private Transform camera;
    public Camera fpsCamera;

    public GameObject impactEffect;

    // Start is called before the first frame update
    private void Start()
    {
        camera = Camera.main.transform;
    }

    public void Shoot()
    {
        Ray gunRay = new Ray(camera.position, camera.forward);
        if (Physics.Raycast(gunRay, out RaycastHit hitInfo, BulletRange))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out Entity enemy))
            {
                enemy.Health -= Damage;
                Debug.Log("hit");
            }
        }
        Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
