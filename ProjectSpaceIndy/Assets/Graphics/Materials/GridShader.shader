Shader "Custom/GridShader" {
    Properties {
    }
    SubShader {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert
        
        static const float PI =  float(3.141592653589793238462643383279503);

        struct Input {
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = float4(step(0.1,abs(sin(IN.worldPos.x * PI))),step(0.1,abs(sin(IN.worldPos.x * PI))),step(0.1,abs(sin(IN.worldPos.x * PI))),1);
        }
        ENDCG
    }
    Fallback "Diffuse"
}
