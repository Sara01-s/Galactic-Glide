using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using UnityEngine;
using System.Linq;

// If you're using this, make sure you have unity audio enabled in Edit -> Project Settings -> Audio
/// <summary> Provides an Unity Audio system facade </summary>
[DefaultExecutionOrder(-50)]
public sealed partial class AudioController : MonoBehaviour, IAudioService {

	public string Prefix { get; set; } = "Audio Service : ";

	[System.Serializable]
	internal class MixerData {
		[field:SerializeField] internal string Name { get; set; }
		[field:SerializeField, Min(0)] internal int BanksAmount { get; set; }
		[field:SerializeField] internal string BankName     { get; set; }
		[field:SerializeField] internal string ExposedValue { get; set; }
		[field:SerializeField] internal Mixer MixerTarget   { get; set; }
		[field:SerializeField] internal AudioMixerGroup MixerGroup { get; set; }
		internal List<AudioSource> AudioBanks { get; set; } = new();
	}

	[Header("Audio Controller Settings")]
	[SerializeField] private GameObject _bankPrefab;
	[SerializeField] private List<MixerData> _mixersData;
	[SerializeField] private AudioMixer _masterMixer;
	[SerializeField] private string _exposedMasterVolume;

	private readonly Dictionary<Mixer, MixerData> _mixersDict = new();

	private void Awake() {
		Services.Instance.RegisterService<IAudioService>(this);

		foreach (var mixerData in _mixersData) {
			Assert.IsNotNull(mixerData.MixerGroup, $"Please assign a mixer group for: {mixerData.Name}");
			
			if (!_mixersDict.TryAdd(mixerData.MixerTarget, mixerData)) {
				Debug.LogError($"{Prefix} Failed to register {mixerData.BankName}");
				continue;
			}
			else 
				Logs.SystemLog($"{Prefix} {mixerData.BankName} data registered");
		}

		foreach (var mixerData in _mixersData) {
			GenerateAllAudioBanksFor(mixerData.MixerTarget);
		}

		Logs.SystemLog(Prefix + " Audio Banks Generated");
	}

	private void OnDisable() {
		Services.Instance.UnRegisterService<IAudioService>();
	}

	private void GenerateAllAudioBanksFor(Mixer mixer) {
		var mixerData = _mixersDict[mixer];

		for (int i = 0; i < mixerData.BanksAmount; i++) {
			var bankObj   = GameObject.Instantiate(_bankPrefab, parent: transform);
			var bankAudio = bankObj.GetComponent<AudioSource>();

			bankAudio.outputAudioMixerGroup = mixerData.MixerGroup;
			bankAudio.Stop(); // For some reason, initialized audio sources have isPlaying set to true by default. :/
			mixerData.AudioBanks.Add(bankAudio);
			bankObj.name = mixerData.BankName;
		}
	}

	private AudioSource GetAvailableSource(List<AudioSource> sources) {
		var candidate = sources.FirstOrDefault(source => !source.isPlaying && source.clip == null);

		if (candidate == null) {
			candidate = sources.FirstOrDefault(source => !source.isPlaying);

			if (candidate != null)
				candidate.clip = null;
		}

		if (candidate == null) {
			Debug.LogError(Prefix + " No banks left to play the requested sound");
			return null;
		}

		return candidate;
	}

	// IAudioService implementation //

	public float MinVolume => 0.00001f;
	public float MaxVolume => 1.0f;

	public AudioSource PlaySound(string soundFileName, Mixer mixer = Mixer.SFX, float volume = 1.0f, float pitch = 1.0f, 
							bool loop = false, float spatialBlend = 0.0f, byte priority = 128) 
	{
		CheckSoundMixerOutput(soundFileName, mixer);
		return PlaySound(NameToAudioClip(soundFileName), mixer, volume, pitch, loop, spatialBlend, priority);
	}

	public AudioSource PlaySound(AudioClip clip, Mixer mixer = Mixer.SFX, float volume = 1.0f, float pitch = 1.0f, 
							bool loop = false, float spatialBlend = 0.0f, byte priority = 128) 
	{
		if (clip == null) {
			return default;
		}

		if (mixer == Mixer.Master) {
			Debug.LogError("Master mixer can't be used to play sounds directly");
			return default;
		}

		var sources = _mixersDict[mixer].AudioBanks;
		var source = GetAvailableSource(sources);

		source.playOnAwake = false;
		source.spatialize = spatialBlend > 0.01f;
		source.spatialBlend = spatialBlend;
		source.pitch = pitch;
		source.clip = clip;
		source.loop = loop;
		source.priority = priority;
		source.volume = volume;

		source.Play();

		return source;
	}

	public void ChangeMixerVolume(Mixer mixer, float newVolume)
	{
		throw new System.NotImplementedException();
	}

	public float GetMixerVolume(Mixer mixer)
	{
		throw new System.NotImplementedException();
	}
}