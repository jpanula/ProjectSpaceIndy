using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour {
	
	// Also in unitbase
	private IMover _mover;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		float horizontal = Input.GetAxisRaw( "Horizontal" );
		float vertical = Input.GetAxisRaw( "Vertical" );

		// Muodostetaan syötteestä vektori
		Vector3 inputVector = new Vector3( horizontal, 0, vertical );

		// Kertomalla inputVector Time.deltaTime:lla saamme fps:stä
		// riippumattoman liikevektorin
		Vector3 movementVector = inputVector;

		// Kutsutaan Moverin Move metodia ja välitetään syötevektori 
		// parametrina.
		Mover.Move( movementVector * Time.deltaTime);

	}
	
	// Remove after unitbase is implemented
	public IMover Mover
	{
		get
		{
			if ( _mover == null )
			{
				_mover = GetComponent< IMover >();
			}

			return _mover;
		}
	}

}
