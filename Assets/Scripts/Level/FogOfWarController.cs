using UnityEngine;

/// <summary>
/// Controls fog of war visibility based on light beam
/// </summary>
public class FogOfWarController : MonoBehaviour
{
    [SerializeField] private Material fogMaterial;
    [SerializeField] private float fogRadius = 15f;
    [SerializeField] private float beamRadiusOffset = 2f;
    
    private SpriteRenderer _fogSprite;
    private PlayerController _playerController;
    
    private void Start()
    {
        _fogSprite = GetComponent<SpriteRenderer>();
        _playerController = FindObjectOfType<PlayerController>();
        
        if (_fogSprite == null)
        {
            _fogSprite = gameObject.AddComponent<SpriteRenderer>();
        }
        
        if (fogMaterial != null)
        {
            _fogSprite.material = new Material(fogMaterial);
        }
    }
    
    private void Update()
    {
        if (_playerController != null)
        {
            UpdateFogOfWar();
        }
    }
    
    private void UpdateFogOfWar()
    {
        Vector3 playerPos = _playerController.transform.position;
        Vector3 beamDir = _playerController.GetLightBeamDirection();
        float beamLength = _playerController.GetBeamLength();
        
        // Update shader parameters for fog effect
        if (fogMaterial != null)
        {
            fogMaterial.SetVector("_PlayerPos", playerPos);
            fogMaterial.SetVector("_BeamDirection", beamDir);
            fogMaterial.SetFloat("_BeamLength", beamLength);
            fogMaterial.SetFloat("_FogRadius", fogRadius);
            fogMaterial.SetFloat("_BeamRadiusOffset", beamRadiusOffset);
        }
    }
}
