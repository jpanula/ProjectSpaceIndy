Shader "Custom/GridShader" {
    Properties {
        _BackgroundColor ("Background Color", Color) = (0,0,0,0)
        _GridColor ("Grid Color", Color) = (1,1,1,1)
        _GridBase ("Units per Gridline", float) = 1
    }
    SubShader {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert
        
        static const float PI =  float(3.141592653589793238462643383279503);

        struct Input {
            float3 worldPos;
        };
        float _GridBase;

        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = float4(pow(abs(sin(IN.worldPos.x * PI / _GridBase)),1.0f/3),pow(abs(sin(IN.worldPos.x * PI / _GridBase)),1.0f/3),pow(abs(sin(IN.worldPos.x * PI / _GridBase)),1.0f/3),1);
        }
        ENDCG
    }
    Fallback "Diffuse"
}
