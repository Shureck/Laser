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
        // �������� ������ �� ��������� RawImage � ���� �������
        _rawImage = GetComponent<RawImage>();

        // ������� ��������, ��������� ��������� ������
        _mat = new Material(_shader);

        // ������� RenderTexture, ������� ����� �������������� ��� ����������� ���������� �������
        _mosaicTexture = new RenderTexture(_xTileCount, Mathf.RoundToInt((float)Screen.height / Screen.width * _xTileCount), 0);
    }

    private void Update()
    {
        // �������� �������� ������ ����
        OnUpdate();
    }

    private void OnUpdate()
    {
        // ���������� ��������� �������
        _mat.SetTexture("_OverlayTex", _overlayTexture);
        _mat.SetColor("_OverlayCol", _overlayCol);
        _mat.SetInt("_XTileCount", _xTileCount);
        _mat.SetInt("_YTileCount", Mathf.RoundToInt((float)Screen.height / Screen.width * _xTileCount));
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        // ��������� ������ ������ ���� RenderTexture ���������� � ������ �� RawImage ��������� �� ����� null
        if (_mosaicTexture != null && _rawImage != null)
        {
            // ��������� ��������� ������ � RenderTexture
            Graphics.Blit(src, _mosaicTexture, _mat);

            // ���������� RenderTexture ��� �������� ��� RawImage
            _rawImage.texture = _mosaicTexture;

            // ���������� �� ������
            Graphics.Blit(_mosaicTexture, dst);
        }
        else
        {
            // ���� ���-�� �� ���, ������ ����������� �������� �����������
            Graphics.Blit(src, dst);
        }
    }

    private void OnDestroy()
    {
        // ���������� ������� ��� ���������� ������
        if (_mosaicTexture != null)
        {
            _mosaicTexture.Release();
        }
    }
}