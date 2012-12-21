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
	L	QWORD ?		; lo-part of register when structure is loaded by MOVAPD
	H	QWORD ?		; hi-part of register
StructXMM2 ENDS

StructXMM4 STRUCT
	LL	DWORD ?		; lo-lo-part of register when structure is loaded by MOVAPD
	LH	DWORD ?		; lo-hi-part
	HL	DWORD ?		; hi-lo-part of register
	HH	DWORD ?		; hi-hi-part of register
StructXMM4 ENDS

.data
;ALIGN 16
tmp1_xmm		StructXMM2<0.0, 0.0>
;ALIGN 16
imageRatio		StructXMM2<0.0, 0.0>	; .H = Y ratio, .L = X ratio
;ALIGN 16
complex_c		StructComplex<0.0, 0.0>
;ALIGN 16
complex_p		StructComplex<0.0, 0.0>
;ALIGN 16
complex_z			StructComplex<0.0, 0.0>
;ALIGN 16
complex_z_prev		StructComplex<0.0, 0.0>
;ALIGN 16
complex_z_next		StructComplex<0.0, 0.0>
;ALIGN 16
complex_tmp			StructXMM2<0.0, 0.0>
;ALIGN 16
complex_fill_0_0	StructXMM2<0.0, 0.0>
;ALIGN 16
complex_fill_2_1	StructXMM2<1.0, 2.0>
;ALIGN 16
complex_fill_4_4	StructXMM2<4.0, 0.0>

;tmp_segment		DWORD ?
;loop_i DWORD 0
;loop_j DWORD 0
.code

DetectSSE3 proc uses ebx ecx edx

	mov		eax, 1 ; function 0x0000001, check for "pni" Prescott New Instructions (SSE3)
	cpuid

	and		ecx, 1 ; mask for get only first bit
	mov		eax, ecx

	ret
DetectSSE3 endp

ProcessJulia proc imageBytesPtr:ptr, imageBytesLength:dword, imageWidth:dword, imageHeight:dword, imageWidthQ:real8, imageHeightQ:real8, rangeXStart:real8, rangeXStop:real8, rangeYStart:real8, rangeYStop:real8, CRe:real8, CIm:real8
	LOCAL sse3presence:DWORD, pixelLevel:DWORD, tmp_segment:DWORD, loop_i:DWORD, loop_j:DWORD;
	;LOCAL tempppp:StructComplex;
	
	CALL	DetectSSE3
	mov		sse3presence, eax

	MOV2XMM		xmm6, 4, mm0

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
	
	MOVAPD		imageRatio, xmm0	; imageRatio.L = Y ratio
									; imageRatio.H = X ratio
	
	;
	; Load complex C
	MOVLPD		xmm0, CRe			; XMM0: | ---- |c.Re|
	MOVHPD		xmm0, CIm			; XMM0: |c.Im|c.Re|
	MOVAPD		complex_c, xmm0

	;
	; For each Y iteration loop
	;
	MOV			ecx, 0
	MOV			loop_i, ecx
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
		MOVLPD		xmm7, imageRatio.L	; XMM0: | ---- |Yratio|
		MOVD		mm0, ecx
		CVTPI2PD	xmm0, mm0			; XMM0: | ---- |i|
		MULPD		xmm7, xmm0			; XMM7: | ---- |Yratio*i|
		MOVLPD		xmm0, rangeYStart	; XMM0: | ---- |Ymin|
		ADDPD		xmm7, xmm0			; XMM7: | ---- |Yratio*i + Ymin|

		MOVDQ2Q		mm0, xmm7			; grab the result
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
			MOVLPD		xmm7, imageRatio.H	; XMM0: | ---- |Xratio|
			MOVD		mm0, ecx
			CVTPI2PD	xmm0, mm0			; XMM0: | ---- |j|
			MULPD		xmm7, xmm0			; XMM7: | ---- |Xratio*j|
			MOVLPD		xmm0, rangeXStart	; XMM0: | ---- |Xmin|
			ADDPD		xmm7, xmm0			; XMM7: | ---- |Xratio*j + Xmin|

			MOVDQ2Q		mm0, xmm7			; grab the result
			MOVQ		complex_p.Re, mm0


			; imitialize computations

			; PUSH ecx #2
			PUSH		ecx
			MOV			ecx, 0			; iterations counter

			XORPS		xmm0, xmm0				; make zero in xmm registers
			MOVAPD		complex_z, xmm0			; z = 0
			MOVAPD		complex_z_prev, xmm0	; z_prev = 0

			MOVAPD		xmm0, complex_p
			MOVAPD		complex_z_next, xmm0	; z_next = p

			MOV			eax, sse3presence
			
			; main computation loop
			compute_g:
				MOVAPD		xmm0, complex_z
				MOVAPD		complex_z_prev, xmm0	; z_prev = z

				MOVAPD		xmm0, complex_z_next
				MOVAPD		complex_z, xmm0			; z = z_next

				;;; g function
				XORPS		xmm0, xmm0
				XORPS		xmm1, xmm1

				MOVHPD		xmm0, complex_c.Im
				MOVLPD		xmm0, complex_z.Im		; XMM0: |c.Im|complex_z.Im|
				MOVHPD		xmm1, complex_z_prev.Im
				MOVLPD		xmm1, complex_z.Im		; XMM1: |z_prev.Im|complex_z.Im|

				MULPD		xmm0, xmm1				; XMM0: |c.Im * z_prev.Im|z.Im^2|
				; debug checkpoint #1

				XORPS		xmm1, xmm1
				XORPS		xmm2, xmm2

				MOVHPD		xmm1, complex_z.Re
				MOVLPD		xmm1, complex_z.Re		; XMM1: |z.Re|z.Re|
				MOVHPD		xmm2, complex_z.Im
				MOVLPD		xmm2, complex_z.Re		; XMM2: |z.Im|z.Re|

				MULPD		xmm1, xmm2				; XMM1: |z.Re * z.Im|z.Re^2|
				MOVAPD		xmm2, complex_fill_2_1
				MULPD		xmm1, xmm2				; XMM1: |z.Re * z.Im * 2|z.Re^2|
				; debug checkpoint 2

				CMP			eax, 1
				JE			compute_g_checkpoint3_sse_found
				JMP			compute_g_checkpoint3_sse_notfound

				compute_g_checkpoint3_sse_found:
					ADDSUBPD	xmm1, xmm0				; XMM1: |(z.Re * z.Im * 2) + (c.Im * z_prev.Im)|(z.Re^2) - (z.Im^2)|
					JMP			compute_g_checkpoint3_end
				
				compute_g_checkpoint3_sse_notfound:
					XORPS		xmm2, xmm2
					XORPS		xmm3, xmm3
					MOVAPD		tmp1_xmm, xmm0
				
					MOVLPD		xmm2, tmp1_xmm.L		; XMM2: | 0 | z.Im^2 |
					MOVHPD		xmm3, tmp1_xmm.H		; XMM3: | c.Im * z_prev.Im | 0 |
					
					SUBPD		xmm1, xmm2
					ADDPD		xmm1, xmm3
				
					JMP			compute_g_checkpoint3_end
					
				; debug checkpoint 3

				compute_g_checkpoint3_end:
				XORPS		xmm0, xmm0				; XMM0: | 0 | 0 |
				MOVLPD		xmm0, complex_c.Im		; XMM0: | 0 |c.Im|

				XORPS		xmm2, xmm2				; XMM2: | 0 | 0 |
				MOVLPD		xmm2, complex_z_prev.Re	; XMM2: | 0 |z_prev.Re|

				MULPD		xmm0, xmm2				; XMM0: | 0 |c.Im * z_prev.Re|
				; debug checkpoint #4

				MOVLPD		xmm2, complex_c.Re		; XMM2: | 0 |c.Re|
				ADDPD		xmm0, xmm2				; XMM0:	| 0 |(c.Im * z_prev.Re) + c.Re|
				; debug checkpoint #5

				ADDPD		xmm1, xmm0				; XMM1: |(z.Re * z.Im * 2) + (c.Im * z_prev.Im)|(z.Re^2) - (z.Im^2) + (c.Im * z_prev.Re) + c.Re|
				; debug checkpoint #6

				MOVAPD		complex_z_next, xmm1	; z_next

				; compute the modulo
				MOVLPD		xmm0, complex_z.Im		; XMM0: | ---- | z.Im |
				MOVHPD		xmm0, complex_z.Re		; XMM0:	| z.Re | z.Im |
				MOVLPD		xmm1, complex_z.Im		; XMM1: | ---- | z.Im |
				MOVHPD		xmm1, complex_z.Re		; XMM1:	| z.Re | z.Im |

				MULPD		xmm0, xmm1				; XMM0: | z.Re ^ 2 | z.Im ^ 2 |
				MOVAPD		complex_tmp, xmm0

				MOVLPD		xmm1, complex_tmp.H		; XMM1: | ---- | z.Re ^ 2 |
				ADDPD		xmm0, xmm1				; XMM0: | ---- | z.Im ^ 2 + z.Re ^ 2 |
				
				INC		ecx

				; compare xmm and xmm6 and store result i EFLAGS
				COMISD		xmm0, xmm6				; XMM0 is modulo, XMM6 is "4"
				
				
				JAE		compute_g_end					; if modulo >= 4 then end
				;JGE		compute_g_end				; if modulo >= 4 then end
				CMP		ecx, 120					
				JGE		compute_g_end				; if iterations >= 120 then end

				JMP		compute_g					; if modulo < 4 && iterations < 120

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
		;MOV		ecx, loop_i
		INC		ecx
		MOV		loop_i, ecx
		CMP		ecx, imageHeight
		JL		for_i

	; end for_i

	mov eax, imageBytesLength

	ret

ProcessJulia endp

end
