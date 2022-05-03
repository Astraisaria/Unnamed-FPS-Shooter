using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private WeaponManager weapon_Manager;

    public float fireRate = 15f;
    private float  nextTimeToFire;
    public float damage = 20f;

    private Camera mainCam;

    [SerializeField]
    private GameObject arrow_Prefab, spear_Prefab;

    [SerializeField]
    private Transform arrow_Bow_StartPosition;

    //public LayerMask blockingLayer;

    void Awake(){
        weapon_Manager = GetComponent<WeaponManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
    }

    void WeaponShoot(){
        if (weapon_Manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE){
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire){
                nextTimeToFire = Time.time + 1f / fireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                BulletFired();
            }
        } else{
            if (Input.GetMouseButtonDown(0)){
                if (weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG){
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                if (weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET){
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                    BulletFired();
                } else{
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                    if (weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.ARROW){
                        ThrowArrowOrSpear(true);
                    } else if(weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.SPEAR){
                        ThrowArrowOrSpear(false);
                    }
                }
            } // if input get mouse button 0
        } // else
    }

    void ThrowArrowOrSpear(bool throwArrow){
        if (throwArrow){
            GameObject arrow = Instantiate(arrow_Prefab);
            arrow.transform.position = arrow_Bow_StartPosition.position;

            arrow.GetComponent<ArrowBowScript>().Launch(mainCam);
        } else{
            GameObject spear = Instantiate(spear_Prefab);
            spear.transform.position = arrow_Bow_StartPosition.position;

            spear.GetComponent<ArrowBowScript>().Launch(mainCam);
        }
    }

    void BulletFired(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)){
            hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
        }
    }
}
