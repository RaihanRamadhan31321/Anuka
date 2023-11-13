using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeImageOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image imageToChange;
    public Sprite newSprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (imageToChange != null && newSprite != null)
        {
            imageToChange.sprite = newSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Jika Anda ingin mengembalikan gambar ke gambar asli saat kursor meninggalkan tombol
        // Tambahan logika sesuai kebutuhan Anda di sini
    }
}
