pipeline {
  agent {
    docker {
      image 'abhishekf5/maven-abhishek-docker-agent:v1'
      args '--user root -v /var/run/docker.sock:/var/run/docker.sock' // mount Docker socket to access the host's Docker daemon
    }
  }
  stages {
    stage('Checkout') {
      steps {
        sh 'echo passed'
        //git branch: 'main', url: 'https://github.com/iam-veeramalla/Jenkins-Zero-To-Hero.git'
      }
    }
    // stage('Build and Test') {
    //   steps {
    //     sh 'ls -ltr'
    //     // build the project and create a JAR file
    //     sh 'cd  mvn clean package'
    //   }
    // }
    // stage('Static Code Analysis') {
    //   environment {
    //     SONAR_URL = "http://0.0.0.0:9000"
    //     // SONAR_AUTH_TOKEN = "4bfc1cec413007d3900019e06207e2567b31f1db"
    //   }
    //   steps {
    //     withCredentials([string(credentialsId: 'sonarqube', variable: 'SONAR_AUTH_TOKEN')]) {
    //       sh 'cd spring-boot-app && mvn sonar:sonar -Dsonar.login=$SONAR_AUTH_TOKEN -Dsonar.host.url=${SONAR_URL}'
    //     }
    //   }
    // }
    stage('Build and Push Docker Image') {
      environment {
        DOCKER_IMAGE = "sushantjadhavhcl/.net_app-cicd:${BUILD_NUMBER}"
        DOCKERFILE_LOCATION = "Dockerfile"
        REGISTRY_CREDENTIALS = credentials('docker-cred')
      }
      steps {
        script {
            sh 'docker build -t ${DOCKER_IMAGE} .'
            def dockerImage = docker.image("${DOCKER_IMAGE}")
            docker.withRegistry('https://index.docker.io/v1/', "docker-cred") {
                dockerImage.push()
            }
        }
      }
    }
    stage('Update Deployment File') {
        environment {
            GIT_REPO_NAME = "java-maven-sonar-argocd-helm-k8s"
            GIT_USER_NAME = "sushantjadhav416"
            // GITHUB_TOKEN = "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"
        }  
        steps {
            withCredentials([string(credentialsId: 'github', variable: 'GITHUB_TOKEN')]) {
                sh '''
                    git config user.email "sushantjadhav416@gmail.com"
                    git config user.name "sushantjadhav416"
                    BUILD_NUMBER=${BUILD_NUMBER}
                    sed -i "s/replaceImageTag/${BUILD_NUMBER}/g" Deployments_files/frontend.yaml
                    git add Deployments_files/deployment.yaml
                    git add -A
                    git commit -m "Update deployment image to version ${BUILD_NUMBER}"
                    git push https://${GITHUB_TOKEN}@github.com/${GIT_USER_NAME}/${GIT_REPO_NAME} HEAD:main
                '''
            }
        }
    }
  }
}