using UnityEngine;
using UnityEngine.UI;

public class SimpleFilter : MonoBehaviour
{
    [SerializeField] private Shader _shader;
    [SerializeField] private int _xTileCount = 100;
    [SerializeField] private Texture2D _overlayTexture;
    [SerializeField] private Color _overlayCol = Color.white;

    public RawImage _rawImage;
    private Material _mat;
    private RenderTexture _mosaicTexture;

    private void Start()
    {
        // Получить ссылку на компонент RawImage в этом объекте
        _rawImage = GetComponent<RawImage>();

        // Создать материал, используя указанный шейдер
        _mat = new Material(_shader);

        // Создать RenderTexture, который будет использоваться для отображения мозаичного эффекта
        _mosaicTexture = new RenderTexture(_xTileCount, Mathf.RoundToInt((float)Screen.height / Screen.width * _xTileCount), 0);
    }

    private void Update()
    {
        // Обновить материал каждый кадр
        OnUpdate();
    }

    private void OnUpdate()
    {
        // Установить параметры шейдера
        _mat.SetTexture("_OverlayTex", _overlayTexture);
        _mat.SetColor("_OverlayCol", _overlayCol);
        _mat.SetInt("_XTileCount", _xTileCount);
        _mat.SetInt("_YTileCount", Mathf.RoundToInt((float)Screen.height / Screen.width * _xTileCount));
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        // Применить фильтр только если RenderTexture существует и ссылка на RawImage компонент не равна null
        if (_mosaicTexture != null && _rawImage != null)
        {
            // Применить мозаичный эффект к RenderTexture
            Graphics.Blit(src, _mosaicTexture, _mat);

            // Установить RenderTexture как текстуру для RawImage
            _rawImage.texture = _mosaicTexture;

            // Отобразить на экране
            Graphics.Blit(_mosaicTexture, dst);
        }
        else
        {
            // Если что-то не так, просто скопировать исходное изображение
            Graphics.Blit(src, dst);
        }
    }

    private void OnDestroy()
    {
        // Освободить ресурсы при завершении работы
        if (_mosaicTexture != null)
        {
            _mosaicTexture.Release();
        }
    }
}