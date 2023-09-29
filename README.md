# InterpretedAssembly

<p align="center">
  <img src="https://i.ibb.co/zNDtKF7/Capture.png">
</p>

```asm
ds msg "Hello"

_start:
	mov r0, 35
	mov r1, 34
	add r0, r1
_pls:
	write msg
```

This Interpreter uses a custom Assembly dialect - Iterkocze® ASSembly. Experience may vary 
