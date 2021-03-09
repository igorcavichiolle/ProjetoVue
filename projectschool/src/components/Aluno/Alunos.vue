<template>
  <div>
    <Titulo
      :btnVoltar="true"
      :texto="
        professorid != undefined
          ? 'Professor: ' + professor.nome
          : 'Todos os Alunos'
      "
    />
    <!-- só vai mostar o botao de adicionar se existir um professor para nao permitir inserir alunos sem professores -->
    <div v-if="professorid != undefined">
      <input
        type="text"
        placeholder="Nome Do Aluno"
        v-model="nome"
        @keyup.enter="addAluno()"
      />
      <button class="btn btnInput" @click="addAluno()">
        Adicionar
      </button>
    </div>

    <table>
      <thead>
        <th>Mat</th>
        <th>Nome</th>
        <th>Opções</th>
      </thead>
      <tbody v-if="alunos.length">
        <tr v-for="(aluno, index) in alunos" :key="index">
          <td class="colPequeno">{{ aluno.id }}</td>
          <!-- <td>{{aluno.id}}</td> -->
          <router-link
            :to="`/alunoDetalhe/${aluno.id}`"
            tag="td"
            style="cursor: pointer"
          >
            {{ aluno.nome }}
            {{ aluno.sobrenome }}
          </router-link>
          <td class="colPequeno">
            <button class="btn btn_Danger" @click="remover(aluno)">
              Remover
            </button>
          </td>
        </tr>
      </tbody>
      <tfoot v-else>
        <tr>
          <td colspan="3" style="text-align: center">
            <h5>Nenhum aluno encontrado</h5>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
</template>

<script>
import Titulo from "../_share/Titulo.vue";

export default {
  components: {
    Titulo,
  },
  data() {
    return {
      titulo: "Aluno",
      //Pegando o Id de um professor quando a tela for carregada pela rota de alunos
      professorid: this.$route.params.prof_id,
      professor: {},
      nome: "Alunos",
      alunos: [],
    };
  },
  created() {
    if (this.professorid) {
      //Se exisitr algum id de professor ele carrega o professor daquele id
      this.carregarProfessores();
      //Carrega alunos de um professor especifico
      this.$http
        //pegando apenas os alunos do id do professor da variavel professorid
        .get(`http://localhost:5000/api/aluno/ByProfessor/${this.professorid}`)
        .then((res) => res.json())
        .then((alunos) => (this.alunos = alunos));
    } else {
      //Carrega todos os alunos
      this.$http
        .get("http://localhost:5000/api/aluno")
        .then((res) => res.json())
        .then((alunos) => (this.alunos = alunos));
    }
  },
  props: {},
  methods: {
    addAluno() {
      let _aluno = {
        nome: this.nome,
        sobrenome: "",
        dataNasc: "",
        //adiciona o professor que esta relalcionado ao novo aluno inserido
        professorID: this.professor.id,
      };

      this.$http
        .post("http://localhost:5000/api/aluno", _aluno)
        .then((res) => res.json())
        .then((alunoReturn) => {
          this.alunos.push(alunoReturn);

          this.nome = "";
        });
    },
    remover(aluno) {
      this.$http.delete(`http://localhost:5000/api/aluno/${aluno.id}`).then(() => {
        let indice = this.alunos.indexOf(aluno);

        this.alunos.splice(indice, 1);
      });
    },
    carregarProfessores() {
      this.$http
        .get("http://localhost:5000/api/professor/" + this.professorid)
        .then((res) => res.json())
        .then((professor) => {
          this.professor = professor;
        });
    },
  },
};
</script>

<style scoped>
input {
  width: calc(100% - 195px);
  border: 0;
  padding: 20px;
  font-size: 1.3em;
  color: #687f7f;
  display: inline;
}
.btnInput {
  width: 150px;
  border: 0px;
  padding: 20px;
  font-display: 1.3em;
  background-color: rgb(116, 115, 115);
  display: inline;
}

.btnInput:hover {
  padding: 20px;
  margin: 0px;
  border: 0px;
}
</style>
