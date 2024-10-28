using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
internal sealed class VVTTrigger2DEvent : MonoBehaviour {
	
	[SerializeField] private bool _useTag = true;
	[SerializeField] private string _tagToDetect;
	[SerializeField] private LayerMask _layerToDetect;

	[SerializeField] private UnityEvent<Collider2D> _onTriggerEnter2D;
	[SerializeField] private UnityEvent<Collider2D> _onTriggerStay2D;
	[SerializeField] private UnityEvent<Collider2D> _onTriggerExit2D;

	private void Awake() {
		GetComponent<Collider2D>().isTrigger = true;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (_useTag) {
			if (other.CompareTag(_tagToDetect)) {
				_onTriggerEnter2D?.Invoke(other);
			}

			return;
		}

		if (IsLayerInLayerMask(other.gameObject.layer, _layerToDetect)) {
			_onTriggerEnter2D?.Invoke(other);
		}
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (_useTag) {
			if (other.CompareTag(_tagToDetect)) {
				_onTriggerStay2D?.Invoke(other);
			}

			return;
		}

		if (IsLayerInLayerMask(other.gameObject.layer, _layerToDetect)) {
			_onTriggerStay2D?.Invoke(other);
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (_useTag) {
			if (other.CompareTag(_tagToDetect)) {
				_onTriggerExit2D?.Invoke(other);
			}

			return;
		}

		if (IsLayerInLayerMask(other.gameObject.layer, _layerToDetect)) {
			_onTriggerExit2D?.Invoke(other);
		}
	}

	bool IsLayerInLayerMask(int layer, LayerMask layerMask) {
		return (layerMask.value & (1 << layer)) != 0;
	}

}