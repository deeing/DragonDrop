using UnityEngine;using System.Collections;public class EggSpawnArea : MonoBehaviour{    [SerializeField]    [Tooltip("Prefab for the draggable and droppable egg.")]    private GameObject eggPrefab;    [SerializeField]    [Tooltip("Bar below which you cannot drop/drag the egg.")]    private Transform droppingBar;    [SerializeField]    [Tooltip("Offset from the center of the mouse where the dragged egg should appear.")]    private Vector3 dragOffset;    [SerializeField]    [Tooltip("Cooldown after dropping egg to drop the next one")]    private float dropCooldown = 1f;    [SerializeField]    private EggCount eggCount;    private bool isDragging = false;    private Egg draggedEgg;    private Egg eggPool;    private bool isCoolingDown = false;    private WaitForSeconds coolDownWait;    private void Start()    {        PrePopulateEgg();        coolDownWait = new WaitForSeconds(dropCooldown);    }    // small object pool for the egg    private void PrePopulateEgg()    {        eggPool = Instantiate(eggPrefab).GetComponent<Egg>();        eggPool.eggCount = eggCount;    }    public void OnMouseDown()    {        if (isCoolingDown)
        {
            return;
        }        if (!eggCount.HasEggs())
        {
            eggCount.DisplayOutOfEggs();
            return;
        }        isDragging = true;        draggedEgg = eggPool;        PrePopulateEgg();        MoveEggToMouse(draggedEgg);    }    public void OnMouseDrag()    {        if (isDragging)        {            MoveEggToMouse(draggedEgg);        }    }    private void MoveEggToMouse(Egg egg)    {        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);        objPosition.z = 0f;        objPosition.y = Mathf.Max(objPosition.y, GetLowestY());        egg.SetPosition(objPosition + dragOffset);    }    // shouldn't be able to go below dropping bar    private float GetLowestY()    {        return droppingBar.position.y + draggedEgg.GetHeight() / 2f - dragOffset.y;    }    public void OnMouseUp()    {        if (isCoolingDown || !isDragging)
        {
            return;
        }        isDragging = false;        draggedEgg.StartFalling();        draggedEgg = null;        isCoolingDown = true;        eggCount.UseEgg();        StartCoroutine(StartCoolDown());    }    private IEnumerator StartCoolDown()
    {
        yield return coolDownWait;
        isCoolingDown = false;
    }}