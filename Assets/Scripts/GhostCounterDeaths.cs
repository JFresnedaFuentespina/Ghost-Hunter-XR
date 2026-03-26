using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Meta.XR.MRUtilityKit;
using TMPro;
using UnityEngine;

public class GhostCounterDeaths : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject hud;
    private TextMeshProUGUI counterText;
    public int deathsCounter = 0;
    public MRUKAnchor.SceneLabels spawnLabels;
    public float minEdgeDistance = 0.3f;
    public float normalOffset;
    public int spawnTry = 10;
    public bool canvasGenerated = false;
    public GhostSpawner ghostSpawner;
    void Start()
    {
        MRUK.Instance.RegisterSceneLoadedCallback(() => StartCoroutine(DelayedCanvasBuild()));
    }

    IEnumerator DelayedCanvasBuild()
    {
        yield return null;
        BuildCanvas();
    }

    void BuildCanvas()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        int currentTry = 0;

        while (currentTry < spawnTry)
        {
            bool hasFoundPosition = room.GenerateRandomPositionOnSurface(
                MRUK.SurfaceType.VERTICAL,
                minEdgeDistance,
                new LabelFilter(spawnLabels),
                out Vector3 pos,
                out Vector3 norm
            );

            if (hasFoundPosition)
            {
                Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
                Vector3 lookDirection = Camera.main.transform.position - randomPositionNormalOffset;
                Quaternion rotation = Quaternion.LookRotation(-lookDirection, Vector3.up);
                GameObject hudInstance = Instantiate(hud, randomPositionNormalOffset, rotation);
                counterText = hudInstance.GetComponentInChildren<TextMeshProUGUI>();
                if (counterText == null)
                    Debug.LogWarning("No se encontró el TextMeshProUGUI en el HUD.");
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
        if (!canvasGenerated || counterText == null) return;

        counterText.text = "Killed: " + deathsCounter;
    }
}
