using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject rayImpactPrefab;
    public Transform spawnPoint;
    public float bulletSpeed = 10f;
    public float maxDistance = 5f;
    private OVRInput.Controller controller = OVRInput.Controller.RTouch; // Right-hand controller
    private OVRInput.Button button = OVRInput.Button.PrimaryIndexTrigger; // Index trigger button
    public LayerMask layerMask;
    public LineRenderer linePrefab;

    // Update is called once per frame
    void Update()
    {
        bool buttonPressed = OVRInput.GetDown(button, controller);
        if (buttonPressed)
        {
            // ShootBullet();
            ShootRay();
        }
    }

    void ShootBullet()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(spawnPoint.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(projectile, 5f); // Destroy the projectile after 5 seconds
    }

    void ShootRay()
    {
        Ray ray = new Ray(spawnPoint.position, spawnPoint.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask);
        Vector3 endpoint = Vector3.zero;
        if (hasHit)
        {
            endpoint = hit.point;
            Quaternion rayImpactRotation = Quaternion.LookRotation(-hit.normal);
            GameObject rayImpact = Instantiate(rayImpactPrefab, hit.point, rayImpactRotation);
            Destroy(rayImpact, 1f);
        }
        else
        {
            endpoint = spawnPoint.position + spawnPoint.forward * maxDistance;
        }
        
        LineRenderer line = Instantiate(linePrefab);
        line.positionCount = 2;
        line.SetPosition(0, spawnPoint.position);
        line.SetPosition(1, endpoint);


        Destroy(line.gameObject, 10f);
    }
}
