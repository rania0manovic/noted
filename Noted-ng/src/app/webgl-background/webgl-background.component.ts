import { Component } from '@angular/core';
import * as THREE from 'three';
import { EffectComposer } from 'three/examples/jsm/postprocessing/EffectComposer.js';
import { RenderPass } from 'three/examples/jsm/postprocessing/RenderPass.js';
import { ShaderPass } from 'three/examples/jsm/postprocessing/ShaderPass.js';
import { ColorCorrectionShader } from 'three/examples/jsm/shaders/ColorCorrectionShader.js';
import { FXAAShader } from 'three/examples/jsm/shaders/FXAAShader.js';
import {WebGLRenderer} from "three";

@Component({
  selector: 'app-webgl-background',
  templateUrl: './webgl-background.component.html',
  styleUrls: ['./webgl-background.component.css']
})
export class WebglBackgroundComponent {
  camera:any;
  scene:any;
  renderer:any;
  clock:any;
  group:any;
  container:any;
  c1:any;
  c2:any;
  fxaaPass:any;

  ngOnInit(){
    this.init();
    requestAnimationFrame(() => this.animate());
  }

  private init() {
    this.container = document.getElementById('container');
    this.camera = new THREE.PerspectiveCamera(45,this.container.offsetWidth/this.container.offsetHeight,1,2000);
    this.scene= new THREE.Scene();
    this.clock = new  THREE.Clock();

    let light = new THREE.HemisphereLight(0xffffff, 1 );
    light.position.set(0,1000,0);
    this.scene.add(light);

    let dlight = new THREE.DirectionalLight(0xffffff, 1 );
    dlight.position.set(1000,1000,-1000);
    this.scene.add(dlight);

    this.group= new THREE.Group();

    let geometry = new THREE.TetrahedronGeometry(2);
    const material = new THREE.MeshStandardMaterial({color:"#ffffff"});
    for (let i = 0; i < 500; i++) {
      let mesh = new THREE.Mesh(geometry,material);
      mesh.position.x=Math.random()*1000-250;
      mesh.position.y=Math.random()*1000-250;
      mesh.position.z=Math.random()*1000-250;


      mesh.rotation.x = Math.random() * Math.PI;
      mesh.rotation.y = Math.random() * Math.PI;
      mesh.rotation.z = Math.random() * Math.PI;
      this.group.add(mesh);
    }
    this.scene.add(this.group);

    this.renderer = new THREE.WebGLRenderer();
    this.renderer.autoClear=false;
    this.renderer.setPixelRatio(window.devicePixelRatio);
    this.renderer.setSize(this.container.offsetWidth, this.container.offsetHeight);
    this.container.appendChild(this.renderer.domElement);

    let renderPass = new RenderPass(this.scene,this.camera);
    renderPass.clearColor = new THREE.Color( 0, 0, 0 );
    renderPass.clearAlpha = 0;

    this.fxaaPass = new ShaderPass( FXAAShader );
    const colorCorrectionPass = new ShaderPass( ColorCorrectionShader );

    this.c1 = new EffectComposer( this.renderer );
    this.c1.addPass( renderPass );
    this.c1.addPass( colorCorrectionPass );

    let pixelRatio = this.renderer.getPixelRatio();
    this.fxaaPass.material.uniforms[ 'resolution' ].value.x = 1 / ( this.container.offsetWidth * pixelRatio );
    this.fxaaPass.material.uniforms[ 'resolution' ].value.y = 1 / ( this.container.offsetHeight * pixelRatio );

    this.c2 = new EffectComposer( this.renderer );
    this.c2.addPass( renderPass );
    this.c2.addPass( colorCorrectionPass );
    this.c2.addPass(this.fxaaPass);

    window.addEventListener( 'resize',()=> this.onResize() );

  }

   onResize() {
     this.camera.aspect = this.container.offsetWidth / this.container.offsetHeight;
     this.camera.updateProjectionMatrix();

     this.renderer.setSize( this.container.offsetWidth, this.container.offsetHeight );
     this.c1.setSize( this.container.offsetWidth, this.container.offsetHeight );
     this.c2.setSize( this.container.offsetWidth, this.container.offsetHeight );

     const pixelRatio = this.renderer.getPixelRatio();

     this.fxaaPass.material.uniforms[ 'resolution' ].value.x = 1 / ( this.container.offsetWidth * pixelRatio );
     this.fxaaPass.material.uniforms[ 'resolution' ].value.y = 1 / ( this.container.offsetHeight * pixelRatio );

     this.renderer.setPixelRatio(pixelRatio);
  }
   animate() {

     requestAnimationFrame(() => this.animate());

     this.group.rotation.y += this.clock.getDelta() * 0.1;

     this.renderer.setClearColor(0x000000);
     this.renderer.clear();

     this.c1.render();
     this.c2.render();

  }
}
