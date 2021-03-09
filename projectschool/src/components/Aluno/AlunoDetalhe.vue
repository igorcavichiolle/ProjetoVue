<template>
  <div v-if="!loading">
    <Titulo :texto="`Aluno: ${aluno.nome}`" :btnVoltar="true">
      <button class="btn btnEditar" @click="habilitarInput()">
        Editar
      </button>
    </Titulo>
    <table>
      <tbody>
        <tr>
          <td class="colPequeno">Matrícula:</td>
          <td>
            <label>
              {{ aluno.id }}
            </label>
          </td>
        </tr>
        <tr>
          <td class="colPequeno">Nome:</td>
          <td>
            <label v-if="habilitaCampo">
              {{ aluno.nome }}
            </label>
            <input v-else v-model="aluno.nome" type="text" />
          </td>
        </tr>
        <tr>
          <td class="colPequeno">Sobrenome:</td>
          <td>
            <label v-if="habilitaCampo">
              {{ aluno.sobrenome }}
            </label>
            <input v-else v-model="aluno.sobrenome" type="text" />
          </td>
        </tr>
        <tr>
          <td class="colPequeno">Data de Nascimento:</td>
          <td>
            <label v-if="habilitaCampo">
              {{ aluno.dataNasc }}
            </label>
            <input v-else v-model="aluno.dataNasc" type="text" />
          </td>
        </tr>
        <tr>
          <td class="colPequeno">Professor:</td>
          <td>
            <label v-if="habilitaCampo"> {{ aluno.professor.nome }} </label>
            <select v-else v-model="aluno.professor.id">
              <option
                v-for="(professor, index) in professores"
                :key="index"
                v-bind:value="professor.id"
              >
                {{ professor.nome }}
              </option>
            </select>
          </td>
        </tr>
      </tbody>
    </table>

    <div style="margin-top: 10px">
      <div v-if="!habilitaCampo">
        <button class="btn btnSalvar" @click="salvar(aluno)">Salvar</button>
        <button class="btn btnCancelar" @click="cancelar()">Cancelar</button>
      </div>
    </div>
  </div>
</template>

<script>
import Titulo from "../_share/Titulo";
export default {
  components: {
    Titulo,
  },
  data() {
    return {
      professores: [],
      aluno: {},
      id: this.$route.params.id,
      habilitaCampo: true,
      loading: true,
    };
  },
  created() {
    this.carregarProfessor();
  },
  methods: {
    carregarProfessor() {
      this.$http
        .get("http://localhost:5000/api/professor")
        .then(res => res.json())
        .then(professor => {
          this.professores = professor;
          // Depois que carregar o professor carregaremos o aluno
          this.carregarAluno();
        });
    },
    carregarAluno() {
      this.$http
        //pegando apenas os alunos do id do professor da variavel professorid
        .get(`http://localhost:5000/api/aluno/${this.id}`)
        .then(res => res.json())
        .then(aluno => {
          this.aluno = aluno;
          // agora minha variavel vai ser false e a página será carregada
          this.loading = false;
        });
    },
    habilitarInput() {
      this.habilitaCampo = false;
    },
    salvar(_aluno) {
      let _alunoEditar = {
        id: _aluno.id,
        nome: _aluno.nome,
        sobrenome: _aluno.sobrenome,
        dataNasc: _aluno.dataNasc,
        professorid: _aluno.professor.id,
      };

      //SEMELHANTE AO POST PASSAMOS UM .PUT COM A URL DA API, O ID DO ALUNO QUE ESTAMOS EDITANDO E O CONTEUDOP DO OBJETO _ALUNOEDITAR
      this.$http
        .put(`http://localhost:5000/api/aluno/${_alunoEditar.id}`, _alunoEditar)
        .then(res => res.json())
        .then(aluno => this.aluno = aluno)
        .then(() => this.habilitaCampo = true);

      this.habilitaCampo = true;
    },
    cancelar() {
      this.habilitaCampo = true;
    },
  },
};
</script>

<style scoped>
.btnEditar {
  float: right;
  background-color: rgb(76, 186, 249);
}
.btnSalvar {
  float: right;
  background-color: rgb(76, 196, 68);
}
.btnCancelar {
  float: left;
  background-color: rgb(249, 186, 92);
}
.colPequeno {
  width: 20%;
}
input,
select {
  margin: 0px;
  padding: 5px 10px;
  font-size: 0.9em;
  font-family: Montserrat;
  border: 1px solid silver;
  border-radius: 5px;
  background-color: rgb(245, 245, 245);
  width: 95%;
}
select {
  height: 38px;
  width: 100%;
}
</style>
