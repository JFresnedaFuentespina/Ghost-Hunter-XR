using Meta.XR.MRUtilityKit;
using TMPro;
using UnityEngine;

public class GhostCounter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject hud;
    private TextMeshProUGUI counterText;
    public int counter;
    public MRUKAnchor.SceneLabels spawnLabels;
    public float minEdgeDistance = 0.3f;
    public float normalOffset;
    public int spawnTry = 10;
    public bool canvasGenerated = false;
    void Start()
    {
        MRUK.Instance.RegisterSceneLoadedCallback(BuildCanvas);
    }

    void BuildCanvas()
    {

        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        int currentTry = 0;

        while (currentTry < spawnTry)
        {
            bool hasFoundPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, minEdgeDistance, new LabelFilter(spawnLabels), out Vector3 pos, out Vector3 norm);

            if (hasFoundPosition)
            {
                Vector3 randomPositionNormalOffset = pos + norm * normalOffset;

                GameObject hudInstance = Instantiate(hud, randomPositionNormalOffset, Quaternion.identity);
                counterText = hudInstance.transform.Find("CounterTxt").GetComponent<TextMeshProUGUI>();
                canvasGenerated = true;
                return;
            }

            currentTry++;
        }

        Debug.LogWarning("No se pudo generar posición para el HUD en la habitación.");
    }
    // Update is called once per frame
    void Update()
    {
        if (!canvasGenerated) return;
        counterText.text = "Spawned: " + counter;
    }
}
