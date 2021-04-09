pipeline{
    agent any
    tools {
        dotnetsdk '.NET 5'
    }
    
    stages {
        stage('Build'){
            steps{
                sh 'dotnet build --configuration Release'
            }
        }
    }
}