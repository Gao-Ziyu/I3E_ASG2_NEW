/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The GunDamage class is used for damages done by gun
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDamage : MonoBehaviour
{
    int layerMask = 1 << 7;

    /// <summary>
    /// bullet damage
    /// </summary>
    public float Damage;
    /// <summary>
    /// bullet range
    /// </summary>
    public float BulletRange;
    /// <summary>
    /// bullet camera
    /// </summary>
    private Transform camera;
    public Camera fpsCamera;

    /// <summary>
    /// shooting effect
    /// </summary>
    public GameObject impactEffect;

    // Start is called before the first frame update
    private void Start()
    {
        camera = Camera.main.transform;
    }

    /// <summary>
    /// shoot function. when shot bullet will deal damage to enemies and having shooting effects
    /// </summary>
    public void Shoot()
    {
        Ray gunRay = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
        if (Physics.Raycast(gunRay, out RaycastHit hitInfo, BulletRange))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out Entity enemy))
            {
                enemy.Health -= Damage;
                Debug.Log(enemy.Health);
            }
        }
        Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
