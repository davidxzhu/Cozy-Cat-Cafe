using System;
using UnityEngine;

namespace CozyCatCafe.Scripts
{
	[RequireComponent(typeof(AudioSource))]
	public class SoundMaster : MonoBehaviour
	{
		private static SoundMaster _master;

		private AudioSource _source;

		public AudioClip ItemPop;
		public AudioClip Menu;
		public AudioClip Invalid;

		private void Awake()
		{
			if (_master == null)
			{
				_master = this;
				_source = GetComponent<AudioSource>();
				DontDestroyOnLoad(this);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public static void Play(Type type)
		{
			if (_master == null)
				return;

			AudioClip clip;
			switch (type)
			{
				case Type.Item:
					clip = _master.ItemPop;
					break;
				case Type.Menu:
					clip = _master.Menu;
					break;
				case Type.Invalid:
					clip = _master.Invalid;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}

			_master._source.Stop();
			_master._source.clip = clip;
			_master._source.Play();
		}

		public enum Type
		{
			Item,
			Menu,
			Invalid,
		}
	}
}