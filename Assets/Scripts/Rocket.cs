using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    [SerializeField] private float rcsThrust = 100f;
    [SerializeField] private float mainThrust = 150f;

    private Rigidbody _rigidbody;

    private AudioSource _audio;

    // Start is called before the first frame update
    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        Thrust();
        Rotate();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Friendly")) {
            print("Hello");
        }else if (other.gameObject.CompareTag("Fuel")) {
            print("Fuel");
        }else if (other.gameObject.CompareTag("Obstacle")) {
            print("Dead");
        }
    }

    private void Rotate() {
        _rigidbody.freezeRotation = true; // manual control
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * (rcsThrust * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * (rcsThrust * Time.deltaTime));
        }
        _rigidbody.freezeRotation = false; // resume physics control
    }

    private void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
            _rigidbody.AddRelativeForce(Vector3.up * (mainThrust * Time.deltaTime));
            if (!_audio.isPlaying) {
                _audio.Play();
            }
        }
        else {
            _audio.Stop();
        }
    }
}