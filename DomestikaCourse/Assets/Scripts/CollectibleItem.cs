using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
	public GameObject lightingParticles;
	public GameObject burstParticles;

	private SpriteRenderer _rederer;
	private Collider2D _collider;
	public int healthRestoration = 3;

	private void Awake()
	{
		_rederer = GetComponent<SpriteRenderer>();
		_collider = GetComponent<Collider2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) {
			//Cure Player
			collision.SendMessageUpwards("AddHealth",healthRestoration);
			//Disable collider -> esto es para que mientras esta destruyendose, el jugador no pueda seguir curandose varias veces
			_collider.enabled = false;
			//visual effects
			_rederer.enabled = false;
			lightingParticles.SetActive(false);
			burstParticles.SetActive(true);

			Destroy(gameObject, 2f);
		}
	}
}
