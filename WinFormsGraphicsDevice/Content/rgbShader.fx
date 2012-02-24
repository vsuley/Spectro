// GLOBAL PARAMETERS TO BE SET FROM C#

// Basic matrices required for the shader to function.
float4x4 World;
float4x4 View;
float4x4 Projection;

// Phong shading
float4 LightPosition;
float LightStrength;
float AmbientReflectance;
float DiffuseReflectance;
float SpecularReflectance;
float Shininess;
float4 SpecularColor;
float3 CameraPosition;

// Full Spectral rendering
float4 FinalRGB;
float4 MaterialRGB;

float4 LightColor;

// DATA STRUCTURES
struct VertexShaderInput
{
    float4 Position : POSITION0;
    float4 Normal : NORMAL0;
    float2 TexCoords : TEXCOORD0;
};

// Structure for the Vertex shader to spit output.
// These values will automatically be interpolated and passed on to the pixel shader.
struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoords : TEXCOORD0;
    float3 Normal : TEXCOORD1;
    float3 WorldPos : TEXCOORD2;
};

// THE SHADING METHODS
// Vertex Shader
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    // "Multiplication will be done in the pre-shader - so no cost per vertex"
    float4x4 viewprojection = mul(View, Projection);
    float4 posWorld = mul(input.Position, World);
    output.Position = mul(posWorld, viewprojection);
    output.TexCoords = input.TexCoords;

    // Passing information on to do both specular AND diffuse calculation in pixel shader
    output.Normal = mul(input.Normal, (float3x3)World);
    output.WorldPos = posWorld;
    
    return output;
}

// Pixel Shader
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float4 outputColor;

    // Some basic ingredients.
    float3 lightVector = normalize(LightPosition - input.WorldPos);
    float3 reflectedRay = reflect( -lightVector, input.Normal);
    float3 viewer = normalize(-input.WorldPos);
    
    // Calculate diffuse component.
    // 
    float diffuseLightIntensity = LightStrength * (AmbientReflectance + DiffuseReflectance * saturate( dot(lightVector, input.Normal)));
    
    // Calculate specular component
    //
    float specularIntensity = LightStrength * SpecularReflectance * pow( saturate( dot(reflectedRay, viewer)), Shininess);

    outputColor = saturate(diffuseLightIntensity * LightColor * MaterialRGB + saturate(specularIntensity  * SpecularColor * LightColor));
    outputColor.a = 1;

    return outputColor;
}


technique TechniqueWithoutTexture
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
