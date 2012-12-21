.686 
.387
.model flat, stdcall 
.xmm

;;;
;
; MOV2XMM macro puts a 32-bit value into XMM register
; using EAX
;
;;;
MOV2XMM MACRO xmmreg, value, mmx_throught
	PUSH		eax
	MOV			eax, value
	MOVD		mmx_throught, eax
	CVTPI2PD	xmmreg, mmx_throught
	POP			eax
ENDM

StructComplex STRUCT
	Re	QWORD ?
	Im	QWORD ?
StructComplex ENDS

StructXMM2 STRUCT
	L	QWORD ?		; lo-part of register when structure is loaded by MOVUPD
	H	QWORD ?		; hi-part of register
StructXMM2 ENDS

StructXMM4 STRUCT
	LL	DWORD ?		; lo-lo-part of register when structure is loaded by MOVUPD
	LH	DWORD ?		; lo-hi-part
	HL	DWORD ?		; hi-lo-part of register
	HH	DWORD ?		; hi-hi-part of register
StructXMM4 ENDS

.data

.code

DetectSSE3 proc uses ebx ecx edx

	mov		eax, 1 ; function 0x0000001, check for "pni" Prescott New Instructions (SSE3)
	cpuid

	and		ecx, 1 ; mask for get only first bit
	mov		eax, ecx

	ret
DetectSSE3 endp

ALIGN 16
ProcessJulia proc imageBytesPtr:ptr, imageBytesLength:dword, offsetStart:dword, offsetStop:dword, imageWidth:dword, imageHeight:dword, imageWidthQ:real8, imageHeightQ:real8, rangeXStart:real8, rangeXStop:real8, rangeYStart:real8, rangeYStop:real8, CRe:real8, CIm:real8
	LOCAL imageRatio	: StructXMM2
	LOCAL complex_p		: StructComplex

	LOCAL tmp_segment:DWORD;
	LOCAL loop_j:DWORD;
	
	; RESEVED REGISTERS:
	; xmm4 - complex_z
	; xmm5 - complex_z_prev
	; xmm6 - complex_z_next
	; xmm7 - complex_c
	
	XORPS	xmm0, xmm0
	MOVUPD  imageRatio, xmm0
	MOVUPD  complex_p, xmm0

	;CALL	DetectSSE3
	;mov		sse3presence, eax

	;
	; Compute the ratio
	;
	MOVHPD		xmm0, rangeXStop	; XMM0: |rangeXStop| ---- |
	MOVLPD		xmm0, rangeYStop	; XMM0: |rangeXStop|rangeYStop|

	MOVHPD		xmm1, rangeXStart	; XMM1: |rangeXStart| ---- |
	MOVLPD		xmm1, rangeYStart	; XMM1: |rangeXStart|rangeYStart|

	SUBPD		xmm0, xmm1			; XMM0: |rangeXStop - rangeXStart|rangeYStop - rangeYStart|
	MOVHPD		xmm1, imageWidthQ	; XMM1: |imageWidth| ---- |
	MOVLPD		xmm1, imageHeightQ	; XMM1: |imageWidth|imageHeight|
	DIVPD		xmm0, xmm1			; XMM0: |(rangeXStop - rangeXStart)/imageWidth|(rangeYStop - rangeYStart)/imageHeight|
	
	MOVUPD		imageRatio, xmm0	; imageRatio.L = Y ratio
									; imageRatio.H = X ratio
	
	;
	; Load complex C
	MOVHPD		xmm7, CRe			; XMM0: | ---- |c.Re|
	MOVLPD		xmm7, CIm			; XMM0: |c.Im|c.Re|

	;
	; For each Y iteration loop
	;
	MOV			ecx, offsetStart
	for_i:
		
		;
		; Compute segment
		; wchich is stored in EDI
		;
		MOV		eax, ecx
		IMUL	eax, imageHeight
		MOV		tmp_segment, eax		; tmp_segment = i * imageHeight
		
		;
		; Compute complex_p.Im
		;
		MOVLPD		xmm1, imageRatio.L	; XMM0: | ---- |Yratio|
		MOVD		mm0, ecx
		CVTPI2PD	xmm0, mm0			; XMM0: | ---- |i|
		MULPD		xmm1, xmm0			; XMM7: | ---- |Yratio*i|
		MOVLPD		xmm0, rangeYStart	; XMM0: | ---- |Ymin|
		ADDPD		xmm1, xmm0			; XMM7: | ---- |Yratio*i + Ymin|

		MOVDQ2Q		mm0, xmm1			; grab the result
		MOVQ		complex_p.Im, mm0

		;
		; For each X iteration loop
		; uses j = ECX for iterator
		;

		; PUSH ecx #1
		PUSH		ecx
		MOV			ecx, 0
		MOV			loop_j, ecx

		fox_j:
			
			;
			; Compute complex_p.Re
			;
			MOVLPD		xmm1, imageRatio.H	; XMM0: | ---- |Xratio|
			MOVD		mm0, ecx
			CVTPI2PD	xmm0, mm0			; XMM0: | ---- |j|
			MULPD		xmm1, xmm0			; XMM7: | ---- |Xratio*j|
			MOVLPD		xmm0, rangeXStart	; XMM0: | ---- |Xmin|
			ADDPD		xmm1, xmm0			; XMM7: | ---- |Xratio*j + Xmin|
			
			MOVDQ2Q		mm0, xmm1			; grab the result
			MOVQ		complex_p.Re, mm0

			MOVUPD		xmm3, complex_p

			; imitialize computations

			; PUSH ecx #2
			PUSH		ecx
			MOV			ecx, 0			; iterations counter

			XORPS		xmm4, xmm4				; z = 0
			XORPS		xmm5, xmm5				; z_prev = 0
			MOVAPD		xmm6, xmm3				; z_next = p

			; main computation loop
			compute_g:
				MOVAPD		xmm5, xmm4	; z_prev = z
				MOVAPD		xmm4, xmm6	; z_prev = z

				;;; g function
				MOVLHPS		xmm0, xmm7				; XMM0: |c.Im| ---- |
				
				MOVAPD		xmm3, xmm4				; XMM3: |complex_z.Im |complex_z.Re |
				MOVHLPS		xmm0, xmm3				; XMM0: |c.Im|complex_z.Im|
				
				MOVHLPS		xmm1, xmm5				; XMM1: | ---- |z_prev.Im|
				MOVLHPS		xmm1, xmm1				; XMM1: |z_prev.Im|z_prev.Im|
				MOVHLPS		xmm1, xmm4				; XMM1: |z_prev.Im|complex_z.Im|

				MULPD		xmm0, xmm1				; XMM0: |c.Im * z_prev.Im|z.Im^2|
				; debug checkpoint #1

				XORPS		xmm1, xmm1
				XORPS		xmm2, xmm2

				MOVAPD		xmm1, xmm4				; XMM1: |z.Im|z.Re|
				MOVLHPS		xmm1, xmm1				; XMM1: |z.Re|z.Re|
				MOVAPD		xmm2, xmm4				; XMM2: |z.Im|z.Re|

				MULPD		xmm1, xmm2				; XMM1: |z.Re * z.Im|z.Re^2|

				MOVAPD		xmm3, xmm1				; XMM3: |z.Re * z.Im|z.Re^2|
				MOVHLPS		xmm3, xmm3				; XMM3: |z.Re * z.Im|z.Re * z.Im|
				ADDPD		xmm3, xmm3				; XMM3: |z.Re * z.Im * 2|z.Re * z.Im * 2|

				MOVLHPS		xmm1, xmm3				; XMM1: |z.Re * z.Im * 2|z.Re^2|
				; debug checkpoint 2

				ADDSUBPD	xmm1, xmm0				; XMM1: |(z.Re * z.Im * 2) + (c.Im * z_prev.Im)|(z.Re^2) - (z.Im^2)|
				JMP			compute_g_checkpoint3_end
				
				; debug checkpoint 3

				compute_g_checkpoint3_end:
				XORPS		xmm0, xmm0				; XMM0: | 0 | 0 |
				MOVAPD		xmm3, xmm7				; XMM3:	|c.Re|c.Im|
				MOVLHPS		xmm3, xmm3				; XMM3:	|c.Im|c.Im|

				MOVHLPS		xmm0, xmm3				; XMM0: | 0 |c.Im|

				XORPS		xmm2, xmm2				; XMM2: | 0 | 0 |
				MOVAPD		xmm3, xmm5				; XMM3: | z_prev.Im | z_prev.Re |
				MOVLHPS		xmm3, xmm3				; XMM3: | z_prev.Re | z_prev.Re |

				MOVHLPS		xmm2, xmm3				; XMM2: | 0 |z_prev.Re|

				MULPD		xmm0, xmm2				; XMM0: | 0 |c.Im * z_prev.Re|
				; debug checkpoint #4

				MOVHLPS		xmm2, xmm7				; XMM2: | 0 |c.Re|
				ADDPD		xmm0, xmm2				; XMM0:	| 0 |(c.Im * z_prev.Re) + c.Re|
				; debug checkpoint #5

				ADDPD		xmm1, xmm0				; XMM1: |(z.Re * z.Im * 2) + (c.Im * z_prev.Im)|(z.Re^2) - (z.Im^2) + (c.Im * z_prev.Re) + c.Re|
				; debug checkpoint #6

				MOVAPD		xmm6, xmm1	; z_next

				; compute the modulo
				MOVUPD		xmm0, xmm4			; XMM0: | z.Re | z.Im |
				MOVAPD		xmm1, xmm4				; XMM1: | z.Re | z.Im |

				MULPD		xmm0, xmm1				; XMM0: | z.Re ^ 2 | z.Im ^ 2 |

				XORPS		xmm1, xmm1				; XMM1: | ---- | ---- |
				MOVHLPS		xmm1, xmm0				; XMM1: | ---- | z.Re ^ 2 |
				ADDPD		xmm0, xmm1				; XMM0: | ---- | z.Im ^ 2 + z.Re ^ 2 |
				
				INC		ecx

				; compare xmm and xmm6 and store result i EFLAGS
				MOV			eax, 4
				MOVD		mm0, eax
				CVTPI2PD	xmm1, mm0		; XMM1: | ---- | 4 | 
				MOVLHPS		xmm1, xmm1		; XMM1: | 4 | 4 |
				COMISD		xmm0, xmm1		; XMM0 is modulo, XMM5 is "4"
				
				JAE		compute_g_end					; if modulo >= 4 then end
				CMP		ecx, 120					

				JNGE		compute_g				; if iterations >= 120 then end

			; end compute g
		
		compute_g_end:
			; move result to memory
			MOV		edx, imageBytesPtr
			MOV		eax, tmp_segment
			ADD		eax, loop_j

			MOV		DWORD PTR[edx+eax*4], ecx

			; PUSH ecx POP #2
			POP		ecx

			; check repetition for_y
			INC		ecx
			MOV		loop_j, ecx
			CMP		ecx, imageWidth
			JL		fox_j
		
		; end for_j	

		; PUSH ecx POP #1
		POP		ecx

		; check repetition for_y
		INC		ecx
		CMP		ecx, offsetStop
		JL		for_i

	; end for_i

	mov eax, 0

	ret

ProcessJulia endp

end
