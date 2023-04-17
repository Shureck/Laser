using UnityEngine;
using UnityEngine.InputSystem;

public class AddCircleToTexture : MonoBehaviour
{
    public Material objectMaterial; // материал объекта, текстуру которого мы будем изменять

    private void Update()
    {
        // Если нажата левая кнопка мыши
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            // Проверяем, попал ли луч в объект
            if (Physics.Raycast(ray, out hit))
            {
                // Получаем координаты точки попадания луча
                Vector2 hitPoint = hit.textureCoord;

                // Создаем материал с текстурой объекта
                Material mat = new Material(objectMaterial);

                // Создаем текстуру круга
                Texture2D circle = new Texture2D(32, 32);
                for (int i = 0; i < circle.width; i++)
                {
                    for (int j = 0; j < circle.height; j++)
                    {
                        if (Mathf.Pow(i - circle.width / 2, 2) + Mathf.Pow(j - circle.height / 2, 2) < Mathf.Pow(circle.width / 2, 2))
                        {
                            circle.SetPixel(i, j, Color.red);
                        }
                        else
                        {
                            circle.SetPixel(i, j, Color.clear);
                        }
                    }
                }
                circle.Apply();

                // Добавляем круг на текстуру объекта
                int x = Mathf.FloorToInt(hitPoint.x * mat.mainTexture.width);
                int y = Mathf.FloorToInt(hitPoint.y * mat.mainTexture.height);
                Texture2D mainTexture = mat.mainTexture as Texture2D;
                mainTexture.SetPixels(x - circle.width / 2, y - circle.height / 2, circle.width, circle.height, circle.GetPixels());
                mainTexture.Apply();

                // Применяем новый материал к объекту
                GetComponent<Renderer>().material = mat;
            }
        }
    }
}