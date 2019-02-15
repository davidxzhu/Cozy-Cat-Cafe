using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CozyCatCafe.Scripts;

[RequireComponent(typeof(SpriteRenderer))]
public class Customers : MonoBehaviour
{
	private State state;

	public Transform customer;
	public Transform dish;
	public Seat seat;
	public float customerMoveSpeed;

	[Header("Food")]
	public DialogueBubble bubble;

	public Food orderDish;

	[NonSerialized]
	public bool gotFood;

	private Vector3 moveStep;
	public float fadeStep;

	private SpriteRenderer _spriteRenderer;
	private Sprite _sitting;
	private Sprite _walking;

	[Header("Sound")]
	private AudioSource _catSource;
	public AudioClip CatHungryClip;
	public AudioClip CatHappyClip;
	public static bool IsPlaying;

	public void SetSprite(Sprite sitting, Sprite walking)
	{
		_sitting = sitting;
		_walking = walking;
		if (_spriteRenderer != null)
		{
			_spriteRenderer.sprite = _walking;
			_spriteRenderer.color = new Color(1, 1, 1, 0f);
		}
	}

	private void Start()
	{
		//createDish();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_spriteRenderer.sprite = _walking;
		_spriteRenderer.color = new Color(1, 1, 1, 0f);

		IsPlaying = false;
	}

	private void Update()
	{
		if (state == State.WalkingIn)
		{
			if (_spriteRenderer.color.a < 1f)
				goToSeat();
			else
			{
				state = State.Ordering;
				_spriteRenderer.sprite = _sitting;
				_spriteRenderer.color = Color.white;
				createDish();
			}
		}

		else if (state == State.Ordering)
		{
			if (!gotFood)
				return;
			else
			{
				state = State.Eating;
				bubble.ChangeSprite(null);
				_catSource = GetComponent<AudioSource>();
				_catSource.volume = 1.0f;
				_catSource.PlayOneShot(CatHappyClip);
			}
		}

		else if (state == State.Eating)
		{
			state = State.WalkingOut;
			seat.Customer = null;
		}

		else
		{
			leave();
		}
	}

	// Used for Spawner
	public void setSeat(Seat seat)
	{
		this.seat = seat;
		seat.Customer = this;
		var delta = customer.position - seat.transform.position;
		var len = delta.magnitude;
		var dir = delta / len;
		moveStep = dir * customerMoveSpeed;
		var time = len / customerMoveSpeed;
		fadeStep = 1 / time;
	}

	void goToSeat()
	{
		customer.position -= moveStep * Time.deltaTime;
		var color = _spriteRenderer.color;
		color.a += fadeStep * Time.deltaTime;
		_spriteRenderer.color = color;
		if (!IsPlaying)
		{
			IsPlaying = true;
			_catSource = GetComponent<AudioSource>();
			_catSource.volume = 0.175f;
			_catSource.PlayOneShot(CatHungryClip);
		}
	}

	void createDish()
	{
		bubble.ChangeSprite(orderDish.Sprite);
	}

	void leave()
	{
		customer.position += moveStep * Time.deltaTime;
		var color = _spriteRenderer.color;
		color.a -= fadeStep * Time.deltaTime;

		if (color.a <= 0)
		{
			Destroy(gameObject);
		}
		else
		{
			_spriteRenderer.color = color;
		}
	}

	public enum State
	{
		WalkingIn,
		Ordering,
		Eating,
		WalkingOut
	}
}